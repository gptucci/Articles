using BlazorToDoList.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorToDoList.Store.ToDo
{
    public class ToDoListAppState
    {
        private List<ToDoItem> _todoitemlist { get; set; }

        public ReadOnlyCollection<ToDoItem> todoitemlist => _todoitemlist.AsReadOnly();
        public ToDoListAppState()
        {
            _todoitemlist = new List<ToDoItem>();
            
        }

        public ToDoListAppState(List<ToDoItem> _todoitemlist)
        {
            this._todoitemlist = _todoitemlist;
        }

    }
}
