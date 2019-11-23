using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorToDoList.Models
{
    public class ToDoItem
    {
        [Required]
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
