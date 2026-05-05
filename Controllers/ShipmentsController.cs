using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudScale.Logic.API.Data;
using CloudScale.Logic.API.Models;

namespace CloudScale.Logic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Veritabanı bağlantısını içeri alıyoruz (Dependency Injection)
        public ShipmentsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Tüm Kargoları Listeleme (GET: api/shipments)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetShipments()
        {
            return await _context.Shipments.ToListAsync();
        }

        // 2. Yeni Kargo Ekleme (POST: api/shipments)
        [HttpPost]
        public async Task<ActionResult<Shipment>> PostShipment(Shipment shipment)
        {
            shipment.TrackingNumber = "CS-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            shipment.CreatedAt = DateTime.UtcNow;

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShipments), new { id = shipment.Id }, shipment);
        }
    }
}