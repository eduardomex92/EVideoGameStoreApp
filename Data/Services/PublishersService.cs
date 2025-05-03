using EVideoGameStoreApp.Data.Base;
using EVideoGameStoreApp.Models;

namespace EVideoGameStoreApp.Data.Services
{
    public class PublishersService : EntityBaseRepository<Publisher>, IPublishersService
    {
        public PublishersService(AppDbContext context): base(context)
        {
            
        }
    }
}