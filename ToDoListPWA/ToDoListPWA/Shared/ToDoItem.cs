using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListPWA.Shared
{

    [Table("ToDoItems")]
    public class ToDoItem
    {
        [Key]
        public string Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime DataOraUltimaModifica { get; set; }
        [Required]
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public bool Done { get; set; }
    }
}
