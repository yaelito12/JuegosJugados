using Microsoft.Data.Sqlite;
using Act.Components;
using Act.Components.Data;
using Act.Components.Servicios;


SQLitePCL.Batteries.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<ServicioControlador>();
builder.Services.AddSingleton<ServicioJuegos>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

string ruta = "mibase.db";
using var conexion = new SqliteConnection($"DataSource={ruta}");
conexion.Open();
var comando = conexion.CreateCommand();
comando.CommandText = @"
CREATE TABLE IF NOT EXISTS Juegos ( 
    Identificador INTEGER PRIMARY KEY, 
    Nombre TEXT, 
    Jugado INTEGER 
)";
comando.ExecuteNonQuery();

app.Run();
