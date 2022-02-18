using SMS.Models;
using System.Collections.Generic;

namespace SMS.Contracts
{
    public interface ICartService
    {

        (bool, string) CleanCart(string userId);

        (bool, string) AddProductToCart(string productId, string userId);

        IEnumerable<CartDetailsViewModel> GetCartProducts(string userId);

        
    }
}
