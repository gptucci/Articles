using BlazorToDoListMVVM.Models;
using BlazorToDoListMVVM.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProjectBlazorToDoListMVVM
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void ToDoItemsListViewModel_NotifyPropertyChanged()
        //{

        //    bool eventWasRaised = false;
        //    var ToDoListViewModel = new ToDoItemsListViewModel();

        //    Assert.IsInstanceOfType(ToDoListViewModel, typeof(INotifyPropertyChanged));

        //    ToDoListViewModel.PropertyChanged += (sender, e) =>
        //    {
        //        //if (e.PropertyName == nameof(ToDoItemsListViewModel.Title))
        //        //{
        //            eventWasRaised = true;
        //        //}
        //    };

        //    //ToDoListViewModel.Title = "Nuoto Titolo";
  

        //    Assert.IsTrue(eventWasRaised);
            
        //}


        [TestMethod]
        public async Task ToDoItemsListViewModel_AddToDoItem()
        {

           

            bool eventWasRaised = false;
            var ToDoListViewModel = new ToDoItemsListViewModel();
            await ToDoListViewModel.Init();

            Assert.IsInstanceOfType(ToDoListViewModel, typeof(INotifyPropertyChanged));

            var NewToDoItem = new ToDoItem
            {
                Description = "Nuova attività"
            };

            ToDoListViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ToDoItemsListViewModel.ToDoItemList))
                {
                    eventWasRaised = true;
                }
            };
            
            await ToDoListViewModel.AddToDoItem(NewToDoItem);

            Assert.IsTrue(eventWasRaised);

            Assert.AreEqual(ToDoListViewModel.ToDoItemList.Count(), 21);

            Assert.AreEqual (ToDoListViewModel.ToDoItemList.LastOrDefault(), NewToDoItem);


           
        }
    }
}
