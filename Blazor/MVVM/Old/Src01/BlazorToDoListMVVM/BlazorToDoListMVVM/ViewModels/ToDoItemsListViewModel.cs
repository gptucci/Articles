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


        public void AddToDoItem(ToDoItem todoitem)
        {

            _ToDoItemList.Add(todoitem);
            OnPropertyChanged(nameof(ToDoItemList));
        }


        private string _Title = string.Empty;

        public string Title
        {
            get => _Title;
            set
            {

                _Title = value;
                OnPropertyChanged();
            }
        }


        private List<ToDoItem> _ToDoItemList = new List<ToDoItem>();

        public List<ToDoItem> ToDoItemList
        {
            get => _ToDoItemList;
            set
            {

                _ToDoItemList = value;
                OnPropertyChanged();
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
