using BlazorToDoListMVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorToDoListMVVM.ViewModels
{
    public class ToDoItemsListViewModel : INotifyPropertyChanged
    {

        public ToDoItemsListViewModel()
        {



        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _IsBusy = false;
        public bool IsBusy
        {
            get => _IsBusy;
            set
            {

                _IsBusy = value;
                OnPropertyChanged();
            }
        }
        public async Task Init()
        {

            IsBusy = true;

            ToDoItemList = Enumerable.Range(1, 20).Select(x => new ToDoItem { Description = "todo " + x }).ToList();

            //Per simulare lunga operazione di insert
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsBusy = false;

        }

        public async Task AddToDoItem(ToDoItem todoitem)
        {
            IsBusy = true;
            _ToDoItemList.Add(todoitem);
            OnPropertyChanged(nameof(ToDoItemList));
            //Per simulare lunga operazione di insert
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsBusy = false;


        }



        private List<ToDoItem> _ToDoItemList = new List<ToDoItem>();

        public List<ToDoItem> ToDoItemList
        {
            get => _ToDoItemList;
            private set
            {

                _ToDoItemList = value;
                OnPropertyChanged();
            }


        }
        public string Title
        {
            get
            {
                return "Totale ToDo: " + ToDoItemList.Count();

            }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
