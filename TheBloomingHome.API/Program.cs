using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TheBloomingHome.API.Data;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ProductContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
);

string AssettsPath =
       Path.GetFullPath(
           Path.Combine(
               AppContext.BaseDirectory,
               @"..\..\..\Assets"));
int LastId = 0;
//string absolutepath = HttpContext.Current.Request.Url.AbsoluteUri;

app.MapPost("/SaveImage", async (context) =>
{
    try
    {
        var form = await context.Request.ReadFormAsync();
        var imageFile = form.Files.GetFile("image");

        if (imageFile == null)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("No file was found in the request.");
            return;
        }

        var imageName = $"Image_{LastId}.jpg";
        var imagePath = Path.Combine(AssettsPath, imageName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }
        
        await context.Response.WriteAsJsonAsync($"{context.Request.Path}/{LastId}");
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500; // Internal Server Error
        await context.Response.WriteAsync($"An error occurred: {ex.Message}");
    }
});

app.MapGet("/GetImage/{id}", async (int id) =>
{

    var imageName = $"Image_{id}.jpg";
    var imagePath = Path.Combine(AssettsPath, imageName);
    if (!File.Exists(imagePath))
    {
        return Results.NotFound();
    }

    // Читаем содержимое файла
    var imageBytes = await File.ReadAllBytesAsync(imagePath);

    // Возвращаем изображение в формате jpeg
    return Results.File(imageBytes, "image/jpeg");
});


app.Run();