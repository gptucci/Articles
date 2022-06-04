using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Fluxor;

namespace BlazorToDoList.Store.ToDo
{
    public class ToDoListAppFeature : Feature<ToDoListAppState>
    {

        public override string GetName() => "ToDoList";
        protected override ToDoListAppState GetInitialState() => new ToDoListAppState();

    }
}
