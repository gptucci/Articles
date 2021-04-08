using Blazored.LocalStorage;
using Microsoft.JSInterop;
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
        const string ToDoItemsLocalStoreLocalStore = "ToDoItemsLocalStoreLocalStore";
        public ToDoListLocalRepo(HttpClient httpClient,  ILocalStorageService ls)
        {
            _httpClient = httpClient;
            _ls = ls;
        }


        async Task<ToDoItemsStore> GetToDoItemsStore()
        {

            var todoitemStore = await _ls.GetItemAsync<ToDoItemsStore>(ToDoItemsLocalStoreLocalStore);

            if (todoitemStore == null)
                todoitemStore = new ToDoItemsStore();

            return todoitemStore;

        }

        public async Task EseguiSync()
        {

            var EventiStore = await GetToDoItemsStore();

            var ListaToDoItemDaSincronizzare = EventiStore.ListaToDoItem.Where(x => x.DataOraUltimaModifica > EventiStore.DataOraUltimaTuplaDaServer);



            if (ListaToDoItemDaSincronizzare.Count() > 0)
            {
                (await _httpClient.PutAsJsonAsync("api/todolist/updatefromclient", ListaToDoItemDaSincronizzare)).EnsureSuccessStatusCode();
                //A questo punto quelli cancellati non servono più
                EventiStore.ListaToDoItem.RemoveAll(x => x.Deleted);
            }


            DateTime DataOraUltimaTupla = EventiStore.DataOraUltimaTuplaDaServer;

            //var ListaOrdiniLocali = await _ls.GetItemAsync<List<Ordini>>("ListaOrdiniLocali");

            var json = await _httpClient.GetFromJsonAsync<List<ToDoItem>>($"api/todolist/getalltodoitems?since={DataOraUltimaTupla:o}");  //Okkio qui DataOraUltimaTupla deve essere senza specificare zona oraria - https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip

            foreach (var itemjson in json)
            {

                var itemlocale = EventiStore.ListaToDoItem.Where(x => x.Id == itemjson.Id).FirstOrDefault();

                if (itemlocale == null)
                {
                    if (itemjson.Deleted)
                    { }
                    else
                    {
                        EventiStore.ListaToDoItem.Add(itemjson);

                    }

                }
                else
                {
                    if (itemjson.Deleted)
                    {
                        EventiStore.ListaToDoItem.Remove(itemlocale);


                    }
                    else
                    {
                        EventiStore.ListaToDoItem[EventiStore.ListaToDoItem.FindIndex(ind => ind.Id == itemjson.Id)] = itemjson;
                    }

                }
            }
            EventiStore.DataOraUltimoSync = DateTime.Now;
            if (json.Count()>0)
            {
                EventiStore.DataOraUltimaTuplaDaServer = json.Max(x => x.DataOraUltimaModifica);
            }
            else
            {
                EventiStore.DataOraUltimaTuplaDaServer = DateTime.MinValue;
            }
            
            await _ls.SetItemAsync<ToDoItemsStore>(ToDoItemsLocalStoreLocalStore, EventiStore);

        }

        public async Task SalvaToDoItem(ToDoItem todoitem) //La max dataora tuple ricevute dal server
        {
            var eventiStore = await GetToDoItemsStore();

            todoitem.DataOraUltimaModifica = DateTime.Now;
            if (string.IsNullOrEmpty(todoitem.Id))
            {
                todoitem.Id = Guid.NewGuid().ToString();
                eventiStore.ListaToDoItem.Add(todoitem);

            }
            else
            {
                
                if (eventiStore.ListaToDoItem.Where(x => x.Id == todoitem.Id).Any())
                {
                    eventiStore.ListaToDoItem[eventiStore.ListaToDoItem.FindIndex(ind => ind.Id == todoitem.Id)] = todoitem;
                }
                else
                {
                    eventiStore.ListaToDoItem.Add( todoitem);
                }


                //if (todoitem.Deleted)
                //    eventiStore.ListaToDoItem.RemoveAll(x => x.Id == todoitem.Id);
                //else
                //    eventiStore.ListaToDoItem[eventiStore.ListaToDoItem.FindIndex(ind => ind.Id == todoitem.Id)] = todoitem;
                //eventiStore.ListaToDoItem[eventiStore.ListaToDoItem.FindIndex(ind => ind.Id == evento.Id)] = evento;
            }

            await _ls.SetItemAsync(ToDoItemsLocalStoreLocalStore, eventiStore);
        }

        
        public async Task<List<ToDoItem>> GetListaToDoItem()
        {
            var todoItemsStore = await GetToDoItemsStore(); 

            return todoItemsStore.ListaToDoItem.Where(x=>x.Deleted==false).OrderBy(x=>x.Titolo).ToList();
        }
 
        public async Task<int> GetNumeroItemDaSincronizzare()
        {
            var todoItemsStore = await GetToDoItemsStore();

            return todoItemsStore.ListaToDoItem.Where(x => x.DataOraUltimaModifica > todoItemsStore.DataOraUltimaTuplaDaServer).Count();


        }

    }
}
