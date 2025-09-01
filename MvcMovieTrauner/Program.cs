﻿using Microsoft.EntityFrameworkCore;
using MvcMovieTrauner.Data;
using MvcMovieTrauner.Models;
using MvcMovieTrauner.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovieTraunerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieTraunerContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieTraunerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieService, MovieService>();

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
