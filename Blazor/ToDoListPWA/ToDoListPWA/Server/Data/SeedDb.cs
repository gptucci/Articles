using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListPWA.Shared;

namespace ToDoListPWA.Server.Data
{
    public class SeedDb
    {
        public static void Initialize(AppDbContext db)
        {
            db.ToDoItems.AddRange(CreateSeedData());
            db.SaveChanges();
        }

        private static IEnumerable<ToDoItem> CreateSeedData()
        {
            List<ToDoItem> ListaEventi = new List<ToDoItem>();


            ToDoItem evento01 = new ToDoItem();
            evento01.Id = Guid.NewGuid().ToString();
            evento01.Titolo = "Titolo Todo 01";
            evento01.Descrizione = "Descrizione todo 01";
            evento01.DataOraUltimaModifica = DateTime.Now;
            ListaEventi.Add(evento01);

            return ListaEventi;        }
    }
}
