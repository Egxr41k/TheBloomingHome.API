using Microsoft.EntityFrameworkCore;
using TheBloomingHome.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddDbContext<ProductContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
);

string AssettsPath = Path.Combine(Environment.CurrentDirectory, "Assets");
if (!Directory.Exists(AssettsPath))
{
    Directory.CreateDirectory(AssettsPath);
}


int LastId = Directory.GetFiles(AssettsPath).Length;

app.MapPost("/SaveImage", async (context) =>
{
    try
    {
        var form = await context.Request.ReadFormAsync();
        var imageFile = form.Files.GetFile("image");

        if (imageFile == null)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("No file was found in the request.");
            return;
        }

        var imageName = $"Image_{LastId}.jpg";
        var imagePath = Path.Combine(AssettsPath, imageName);
        

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        await context.Response.WriteAsJsonAsync(
            $"{context.Request.Scheme}://{context.Request.Host}/GetImage/{LastId}");

        LastId++;
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
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

    var imageBytes = await File.ReadAllBytesAsync(imagePath);

    return Results.File(imageBytes, "image/jpeg");
});

app.MapDelete("/DeleteImage/{id}", async(int id) =>
{
    try
    {
        var imageName = $"Image_{id}.jpg";

        string imagePath = Path.Combine(AssettsPath, imageName);
        if (File.Exists(imagePath)) File.Delete(imagePath);
        return Results.Ok($"Image {id} has been deleted.");
    }
    catch (Exception ex)
    {
        return Results.NotFound();
    }
});


app.Run();