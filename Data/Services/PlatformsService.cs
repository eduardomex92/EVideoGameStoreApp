using EVideoGameStoreApp.Data.Base;
using EVideoGameStoreApp.Models;

namespace EVideoGameStoreApp.Data.Services
{
    public class PlatformsService: EntityBaseRepository<Platform>, IPlatformsService
    {
        public PlatformsService(AppDbContext context) : base(context)
        {
            
        }
    }
}
