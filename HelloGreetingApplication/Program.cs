﻿using BusinessLayer.Interface;
using BusinessLayer.Service;
using NLog;
using NLog.Web;
using HelloGreetingApplication.BusinessLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Service;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;

//Implementing NLogger
var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Application is starting...");

    var builder = WebApplication.CreateBuilder(args);

    //Configure NLog

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Register Swagger

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add services to the container.

    builder.Services.AddControllers();

    //Registering the GreetingService
    //builder.Services.AddScoped<IGreetingService, GreetingService>();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();
    
    var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
    builder.Services.AddDbContext<HelloGreetingContext>(options => options.UseSqlServer(connectionString));

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();


    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped due to an exception."); 
    throw;
}
finally
{
    LogManager.Shutdown();
}