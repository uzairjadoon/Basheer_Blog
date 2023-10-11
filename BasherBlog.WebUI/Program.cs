using BasherBlog.Data;
using BasherBlog.Repository;
using BasherBlog.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BasheerContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("Dbbasheer"));

}, ServiceLifetime.Transient);

builder.Services.AddTransient<IUserAccount,AccountRepository>(p=> new AccountRepository (builder.Services.BuildServiceProvider().GetService<BasheerContext>()));
builder.Services.AddTransient<BasherBlog.Repository.IUser, UserRepository>(p => new UserRepository(builder.Services.BuildServiceProvider().GetService<BasheerContext>()));
builder.Services.AddTransient<IPost, PostRepository>(p => new PostRepository(builder.Services.BuildServiceProvider().GetService<BasheerContext>()));

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
