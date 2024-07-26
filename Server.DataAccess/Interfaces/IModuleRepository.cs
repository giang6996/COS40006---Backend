using Module = Server.Common.Models.Module;

namespace Server.DataAccess.Interfaces
{
    public interface IModuleRepository
    {
        Task<Module> GetModuleByModuleName(Common.Enums.Module module);
    }
}