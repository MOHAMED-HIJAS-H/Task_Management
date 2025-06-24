using Microsoft.EntityFrameworkCore;
using Task_Management.Models; // Replace with your actual project namespace
using Task_Management.Data; // ✅ Match your namespace and folder


var builder = WebApplication.CreateBuilder(args);

// ✅ THIS is where you register services
builder.Services.AddControllersWithViews();

// ✅ ADD your DbContext registration here
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer("Server=.;Database=Task;Trusted_Connection=True;TrustServerCertificate=True");
});

builder.Services.AddAuthentication("CustomAuth")
    .AddCookie("CustomAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });





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
