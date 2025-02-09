using Microsoft.AspNetCore.DataProtection;
using System.Text;
using UserMicroservice;
using UserMicroservice.Services;



var builder = WebApplication.CreateBuilder(args);



Startup startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run(); // Запуск приложения