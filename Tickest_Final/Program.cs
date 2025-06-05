using Microsoft.EntityFrameworkCore;
using Tickest_Final.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuraci�n de la cadena de conexi�n a la base de datos
builder.Services.AddDbContext<ticketsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(""))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es 30 d�as. Podr�as cambiarlo para escenarios de producci�n.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Necesario para manejar las rutas
app.UseRouting();

// Para manejar la autorizaci�n
app.UseAuthorization();

// Configura los endpoints de la aplicaci�n
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
