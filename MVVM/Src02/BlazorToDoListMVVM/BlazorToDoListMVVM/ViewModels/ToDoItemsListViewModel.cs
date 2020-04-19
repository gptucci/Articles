using BlazorToDoListMVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorToDoListMVVM.ViewModels
{
    public class ToDoItemsListViewModel 
    {

        public ToDoItemsListViewModel()
        {
            


        }

  
        public bool IsBusy { get; set; }
        
        public async Task Init()
        {

            IsBusy = true;

            ToDoItemList=Enumerable.Range(1, 20).Select(x => new ToDoItem { Description = "todo " + x}).ToList();

            //Per simulare lunga operazione di insert
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsBusy = false;

        }

        public async Task AddToDoItem(ToDoItem todoitem)
        {
            IsBusy = true;

            ToDoItemList.Add(todoitem);

            //Per simulare lunga operazione di insert
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsBusy = false;
            
        }


        public string Title { get { 
            return "Totale ToDo: " + ToDoItemList.Count();

            } 
        }
        
        public List<ToDoItem> ToDoItemList { get; private set; }

    }
}
