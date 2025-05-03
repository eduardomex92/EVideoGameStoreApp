using System.ComponentModel.DataAnnotations;

namespace EVideoGameStoreApp.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public VideoGame VideoGame { get; set; }
        public int Amount { get; set; }


        // ShoppingCartId is a unique identifier for the shopping cart
        public string ShoppingCartId { get; set; }
    }
}
