using Microsoft.EntityFrameworkCore;
using CloudScale.Logic.API.Data; // Senin verdiðin namespace yolunu ekledik

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Baðlantýsýný Yapýlandýr (Render üzerindeki o 'Internal URL'i okur)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// 2. OTOMATÝK VERÝTABANI KURULUMU VE TEMÝZLÝK BLOÐU
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated(); // Sadece yoksa oluþturur, varsa dokunmaz.
        Console.WriteLine(">>> Ohio Veritabaný Hazýr! <<<");
    }
    catch (Exception ex)
    {
        Console.WriteLine(">>> HATA: " + ex.Message);
    }
}
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();