using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using WereWolfMud.Data;
using MudBlazor.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using WereWolfUltraCool;
using WereWolfUltraCool.SignalR;
using WereWolfUltraCool.Interfaces;
using WereWolfUltraCool.Services;
using WereWolfMud.Repositories;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.ResponseCompression;
using static WereWolfMud.Services.UnhandledExceptionLogger;
using Microsoft.AspNetCore.SignalR;
using WereWolfMud.Filter;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
Debug.WriteLine("sex");


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddSingleton<IHubFilter, ExceptionLoggingFilter>();

var unhandledExceptionSender = new UnhandledExceptionSender();
var unhandledExceptionProvider = new UnhandledExceptionProvider(unhandledExceptionSender);
builder.Logging.AddProvider(unhandledExceptionProvider);
builder.Services.AddSingleton<IUnhandledExceptionSender>(unhandledExceptionSender);

builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IGameMakerService, GameMakerService>();
builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();


builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});



string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WereContext>(options
=> options.UseSqlServer(connectionString));

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

var app = builder.Build();
app.UseResponseCompression();
app.MapHub<GameHub>(GameHub.Path);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Migration
using (var scope = app.Services.CreateScope())
{
    var playerContextService = scope.ServiceProvider.GetService<WereContext>();

    try
    {
        playerContextService.Database.EnsureDeleted(); // deocher our recommecner
        playerContextService.Database.Migrate();
    }
    catch(Exception e)
    {

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation(e.Message);

    }
    // this deletes and recreates the whole databse when it is launched.

     var ps = scope.ServiceProvider.GetService<IPlayerService>();
    var s = await ps.AddPlayerToLobbyAndStartGameIfFull(Guid.NewGuid(), "fred");


}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();