using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListPWA.Shared
{
    public class ToDoItemsStore
    {
        public ToDoItemsStore()
        {
            DataOraUltimaTuplaDaServer = DateTime.MinValue;
            DataOraUltimoSync = DateTime.MinValue;
            ListaToDoItem = new List<ToDoItem>();
        }

        public List<ToDoItem> ListaToDoItem { get; set; }
        public DateTime DataOraUltimaTuplaDaServer { get; set; }
        public DateTime DataOraUltimoSync { get; set; }
    }
}
