using BlazorToDoListMVVM.Models;
using MvvmBlazor.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorToDoListMVVM.ViewModels
{
    public class ToDoItemsListViewModel : ViewModelBase
    {

        public ToDoItemsListViewModel()
        {
            


        }


        public void AddToDoItem(ToDoItem todoitem)
        {

            ToDoItemList.Add(todoitem);
        }


        private string _Title = string.Empty;

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
                
        }




        public ObservableCollection<ToDoItem> ToDoItemList { get; } = new ObservableCollection<ToDoItem>();


        //public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    if (PropertyChanged == null)
        //        return;

        //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}


        public override void OnInitialized()
        {
            base.OnInitialized();
            Title = "Lista ToDoItem";
            //ViewModel.Title = "Lista ToDoItem";
            //ViewModel.PropertyChanged += (o, e) => InvokeAsync(() =>
            //      {

            //          StateHasChanged();
            //      }); ;
        }

    }
}
