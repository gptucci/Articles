using BlazorToDoListMVVMLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BlazorToDoListMVVMLib.Service;
using System.Collections.ObjectModel;

namespace BlazorToDoListMVVMLib.ViewModels
{
    public class ToDoItemsListViewModel : INotifyPropertyChanged
    {

        public ToDoItemsListViewModel()
        {


            SalvaCommand = new DelegateCommand((x) => AddToDoItem(x), x => !IsBusy);
        }


        public DelegateCommand SalvaCommand { get; set; }

        public bool IsBusy { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        private void AddToDoItem(object todoitem)
        {
            IsBusy = true;
            ToDoItem toDo = todoitem as ToDoItem;




            _ToDoItemList.Add(toDo);
            OnPropertyChanged(nameof(ToDoItemList));
            IsBusy = false;
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


        private ObservableCollection<ToDoItem> _ToDoItemList = new ObservableCollection<ToDoItem>();

        public ObservableCollection<ToDoItem> ToDoItemList
        {
            get => _ToDoItemList;
            set
            {

                _ToDoItemList = value;
                OnPropertyChanged();
            }
        }


        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
