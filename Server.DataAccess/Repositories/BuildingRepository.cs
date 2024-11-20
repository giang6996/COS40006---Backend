using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly AppDbContext _db;

        public BuildingRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Building>> SearchBuildingsAsync(string query)
        {
            return await _db.Buildings
                .Where(b => b.BuildingName.Contains(query) || b.BuildingAddress.Contains(query))
                .ToListAsync();
        }
        public async Task<Building> GetByIdAsync(long buildingId)
        {
            return await _db.Buildings.FirstOrDefaultAsync(b => b.Id == buildingId)
                ?? throw new KeyNotFoundException("Building not found");
        }
    }
}
