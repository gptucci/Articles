using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToDoListPWA.Shared;

namespace ToDoListPWA.Client.Data
{
    public class ToDoListLocalRepo
    {
        private readonly HttpClient _httpClient;

        private readonly ILocalStorageService _ls;
        private const string ToDoItemsLocalStoreLocalStore = "ToDoItemsLocalStoreLocalStore";

        public ToDoListLocalRepo(HttpClient httpClient, ILocalStorageService ls)
        {
            _httpClient = httpClient;
            _ls = ls;
        }

        public async Task SalvaToDoItem(ToDoItem todoitem)
        {
            var todoitemstore = await GetToDoItemsStore();

            todoitem.DataOraUltimaModifica = DateTime.Now;
            if (string.IsNullOrEmpty(todoitem.Id))
            {
                todoitem.Id = Guid.NewGuid().ToString();
                todoitemstore.ListaToDoItem.Add(todoitem);
            }
            else
            {
                if (todoitemstore.ListaToDoItem.Where(x => x.Id == todoitem.Id).Any())
                {
                    todoitemstore.ListaToDoItem[
                        todoitemstore.ListaToDoItem.FindIndex(ind => ind.Id == todoitem.Id)] = todoitem;
                }
                else
                {
                    todoitemstore.ListaToDoItem.Add(todoitem);
                }
            }

            await _ls.SetItemAsync(ToDoItemsLocalStoreLocalStore, todoitemstore);
        }

        private async Task<ToDoItemsStore> GetToDoItemsStore()
        {
            var todoitemStore = await _ls.GetItemAsync<ToDoItemsStore>(ToDoItemsLocalStoreLocalStore);

            if (todoitemStore == null)
                todoitemStore = new ToDoItemsStore();

            return todoitemStore;
        }

        public async Task EseguiSync()
        {
            var ToDoItemStore = await GetToDoItemsStore();
            DateTime DataOraUltimaTuplaDaServer = ToDoItemStore.DataOraUltimaTuplaDaServer;

            var ListaToDoItemDaSincronizzare = ToDoItemStore.ListaToDoItem.Where(x => x.DataOraUltimaModifica > ToDoItemStore.DataOraUltimaTuplaDaServer);

            if (ListaToDoItemDaSincronizzare.Count() > 0)
            {
                (await _httpClient.PutAsJsonAsync("api/todolist/updatefromclient", ListaToDoItemDaSincronizzare)).EnsureSuccessStatusCode();
                //A questo punto quelli cancellati non servono più
                ToDoItemStore.ListaToDoItem.RemoveAll(x => x.Deleted);
            }

            var json = await _httpClient.GetFromJsonAsync<List<ToDoItem>>($"api/todolist/getalltodoitems?since={DataOraUltimaTuplaDaServer:o}");

            foreach (var itemjson in json)
            {
                var itemlocale = ToDoItemStore.ListaToDoItem.Where(x => x.Id == itemjson.Id).FirstOrDefault();

                if (itemlocale == null)
                {
                    if (itemjson.Deleted)
                    { }
                    else
                    {
                        ToDoItemStore.ListaToDoItem.Add(itemjson);
                    }
                }
                else
                {
                    if (itemjson.Deleted)
                    {
                        ToDoItemStore.ListaToDoItem.Remove(itemlocale);
                    }
                    else
                    {
                        ToDoItemStore.ListaToDoItem[ToDoItemStore.ListaToDoItem.FindIndex(ind => ind.Id == itemjson.Id)] = itemjson;
                    }
                }
            }

            if (json.Count() > 0)
            {
                ToDoItemStore.DataOraUltimaTuplaDaServer = json.Max(x => x.DataOraUltimaModifica);
            }

            await _ls.SetItemAsync<ToDoItemsStore>(ToDoItemsLocalStoreLocalStore, ToDoItemStore);
        }

        public async Task<List<ToDoItem>> GetListaToDoItem()
        {
            var todoItemsStore = await GetToDoItemsStore();

            return todoItemsStore.ListaToDoItem.Where(x => x.Deleted == false).OrderBy(x => x.Titolo).ToList();
        }

        public async Task<int> GetNumeroItemDaSincronizzare()
        {
            var todoItemsStore = await GetToDoItemsStore();

            return todoItemsStore.ListaToDoItem.Where(x => x.DataOraUltimaModifica > todoItemsStore.DataOraUltimaTuplaDaServer).Count();
        }
    }
}