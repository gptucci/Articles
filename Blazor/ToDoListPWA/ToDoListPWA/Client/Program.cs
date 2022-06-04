using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoListPWA.Client.Data;

namespace ToDoListPWA.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("httpclient01",
               a => a.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)); //Aggiunge httpclietfactory
            builder.Services.AddScoped(a => a.GetRequiredService<IHttpClientFactory>().CreateClient("httpclient01"));

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ToDoListLocalRepo>();
            await builder.Build().RunAsync();
        }
    }
}
