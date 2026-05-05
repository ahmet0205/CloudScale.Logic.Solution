using Microsoft.EntityFrameworkCore;
using CloudScale.Logic.API.Models;

namespace CloudScale.Logic.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanındaki tablonun adı "Shipments" olacak
        public DbSet<Shipment> Shipments { get; set; }
    }
}