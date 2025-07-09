using Microsoft.EntityFrameworkCore;
using Task_Management.Domain.Interfaces;
using Task_Management.Service;
using Task_Management.DataAccess;
using Task_Management.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ THIS is where you register services
builder.Services.AddControllersWithViews();

// ✅ ADD your DbContext registration here
//builder.Services.AddDbContext<UserContext>(options =>
//{
//    options.UseSqlServer("Server=.;Database=Task;Trusted_Connection=True;TrustServerCertificate=True");
//});

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("CustomAuth")
    .AddCookie("CustomAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });


builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();



// ✅ Register MemoryDataStore here
builder.Services.AddSingleton<MemoryDataStore>();
var app = builder.Build();

// 👇 Middleware config
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

// Default route
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//modify the route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
