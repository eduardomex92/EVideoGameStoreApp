using EVideoGameStoreApp.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EVideoGameStoreApp.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task <List<Order>> GetOrdersByUserIdAndRoleAsync(String userId,string userRole);

    }
}
