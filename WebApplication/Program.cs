using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication.Data;
using WebApplication.Areas.Identity.Data;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddValidation();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddDefaultIdentity<WebApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WebApplicationContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
