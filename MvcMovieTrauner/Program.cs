using Microsoft.EntityFrameworkCore;
using MvcMovieTrauner.Data;
using MvcMovieTrauner.Features.Movies.Services;
using MvcMovieTrauner.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovieTraunerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieTraunerContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieTraunerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options => options.ViewLocationExpanders.Add(new MvcMovieTrauner.Infrastructure.FeatureViewLocationExpander()));

builder.Services.AddScoped<MvcMovieTrauner.Features.Movies.Services.IMovieService,
                           MvcMovieTrauner.Features.Movies.Services.MovieService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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
app.MapControllers();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
