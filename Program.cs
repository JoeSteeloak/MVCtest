var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();  // Aktivera MVC

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"
);



app.Run();
