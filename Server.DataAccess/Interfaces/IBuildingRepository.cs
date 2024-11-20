using Server.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess.Interfaces
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<Building>> SearchBuildingsAsync(string query);
        Task<Building> GetByIdAsync(long buildingId);
    }
}
