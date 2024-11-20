using Server.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> SearchApartmentsAsync(long buildingId, string query);
        Task<Apartment> GetByIdAsync(long apartmentId);
    }
}
