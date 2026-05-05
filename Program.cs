using Microsoft.EntityFrameworkCore;
using CloudScale.Logic.API.Data; 

var builder = WebApplication.CreateBuilder(args);

// Baðlantý cümlesini alýyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseDefaultFiles(); // index.html'i otomatik bulmasýný saðlar
app.UseStaticFiles();  // HTML/CSS dosyalarýný sunmamýza izin verir

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>(); // Kendi DbContext adýný buraya yaz
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Veritabaný oluþturulurken hata: " + ex.Message);
    }
}

app.Run();