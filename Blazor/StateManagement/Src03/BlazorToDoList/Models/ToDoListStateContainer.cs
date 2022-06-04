using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorToDoList.Models
{
    public class ToDoListStateContainer
    {
        public void AddToDoItem(ToDoItem toDoItem)
        {

            _todoitemlist.Add(toDoItem);
            OnToDoListChanged?.Invoke();
        }
        private List<ToDoItem> _todoitemlist  = new List<ToDoItem>();
        public ReadOnlyCollection<ToDoItem> todoitemlist => _todoitemlist.AsReadOnly();
        
        public event Action OnToDoListChanged;
    }
}
