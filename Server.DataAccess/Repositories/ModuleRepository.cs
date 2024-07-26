using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _db;

        public ModuleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Common.Models.Module> GetModuleByModuleName(Common.Enums.Module module)
        {
            return await _db.Modules.Where(m => m.ModuleName == module.ToString()).FirstOrDefaultAsync() ?? throw new Exception("Module not exist");
        }
    }
}