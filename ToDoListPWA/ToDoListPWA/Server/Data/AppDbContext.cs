using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListPWA.Shared;

namespace ToDoListPWA.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            
             DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
