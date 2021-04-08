using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListPWA.Server.Data;
using ToDoListPWA.Shared;

namespace ToDoListPWA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoListController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbContext _appDbContext;

        public ToDoListController(ILogger<WeatherForecastController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateFromClient(List<ToDoItem> todoitems)
        {
            //https://www.learnentityframeworkcore.com/dbcontext/modifying-data
            foreach (var todoitem in todoitems)
            {

                var todoitemdadb = await _appDbContext.ToDoItems.Where(x => x.Id == todoitem.Id).FirstOrDefaultAsync();
                if (todoitemdadb == null)
                {

                    if (!todoitem.Deleted)
                    {
                        //Ordine da inserire
                        _appDbContext.ToDoItems.Add(todoitem);

                    }


                }
                else
                {
                    //https://docs.microsoft.com/en-us/ef/core/saving/disconnected-entities
                    if (todoitemdadb.DataOraUltimaModifica < todoitem.DataOraUltimaModifica)
                    {
                        _appDbContext.Entry(todoitemdadb).CurrentValues.SetValues(todoitem);

                        //_appDbContext.ToDoItems.Attach(todoitem);
                        //_appDbContext.ToDoItems.Update(todoitem);
                    }

                }
                

            }
            await _appDbContext.SaveChangesAsync();
            //try
            //{
            //    await _appDbContext.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}


            return Ok();
        }



        [HttpGet]
        public async Task<List<ToDoItem>> GetAllTodoitems([FromQuery] DateTime since)
        {

            return await _appDbContext.ToDoItems.Where(x => x.DataOraUltimaModifica > since).ToListAsync();

            
        }
    }
}
