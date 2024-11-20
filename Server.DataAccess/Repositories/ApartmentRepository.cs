using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _db;

        public ApartmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Apartment>> SearchApartmentsAsync(long buildingId, string query)
        {
            return await _db.Apartments
                .Where(a => a.BuildingId == buildingId && a.RoomNumber.ToString().Contains(query))
                .ToListAsync();
        }

        public async Task<Apartment> GetByIdAsync(long apartmentId)
        {
            return await _db.Apartments.FirstOrDefaultAsync(a => a.Id == apartmentId)
                ?? throw new KeyNotFoundException("Apartment not found");
        }
    }
}
