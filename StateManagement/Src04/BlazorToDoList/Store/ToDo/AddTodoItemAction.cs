using BlazorToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorToDoList.Store.ToDo
{


    public class AddTodoItemAction
    {

        public readonly ToDoItem todoitem;
        public AddTodoItemAction(ToDoItem _ToDoItem)
        {
            todoitem = _ToDoItem;
        }
    }
}
