var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();  // Aktivera MVC

// Lägg till sessionhantering
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Hur länge sessionen är aktiv
    });

var app = builder.Build();

app.UseSession(); 
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"
);

app.Run();
