using Diplomski.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Diplomski.Repositories.Implementation;
using Diplomski.Repositories.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//for identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
options.LoginPath = "/UserAuthentication/Login");

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

app.Run();
