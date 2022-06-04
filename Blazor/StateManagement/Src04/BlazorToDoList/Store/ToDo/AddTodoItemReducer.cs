using Blazor.Fluxor;
using BlazorToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorToDoList.Store.ToDo
{
    public class AddTodoItemActionReducer : Reducer<ToDoListAppState, AddTodoItemAction>
    {

        public override ToDoListAppState Reduce(ToDoListAppState state, AddTodoItemAction action)
        {
            var newShortlist = new List<ToDoItem>(state.todoitemlist);
            newShortlist.Add(action.todoitem);
            return new ToDoListAppState(newShortlist);
        }

    }
}
