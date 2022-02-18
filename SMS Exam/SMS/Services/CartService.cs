using Microsoft.EntityFrameworkCore;
using SMS.Contracts;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services
{
    public class CartService : ICartService
    {
        private readonly SMSDbContext dbContext;

        public CartService(SMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public (bool, string) AddProductToCart(string productId, string userId)
        {
            bool isAdded = false;
            Product product = dbContext.Products.FirstOrDefault(p => p.Id == productId);

            //Eager loading is used
            var user =
                   dbContext.Users
                  .Where(u => u.Id == userId)
                  .Include(u => u.Cart)
                  .ThenInclude(c => c.Products)
                  .FirstOrDefault();

            if (product == null)
            {
                return (isAdded, "Product with provided id does no exists.");
            }

            if (user == null)
            {
                return (isAdded, "User with provided id does no exists.");
            }

            isAdded = true;
            user.Cart.Products.Add(product);
            dbContext.SaveChanges();

            return (isAdded, null);
        }



        public (bool, string) CleanCart(string userId)
        {
            bool isCleaned = false;

            User user =
                dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();

            if(user == null)
            {
                return (isCleaned, "User with provided 'id' is not found!");
            }

            user.Cart.Products.Clear();

            dbContext.SaveChanges();
            isCleaned = true;

            return (isCleaned, null);

        }

        public IEnumerable<CartDetailsViewModel> GetCartProducts(string userId)
        {
            var user =
                dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();

            var cartProducts =
                user
                .Cart
                .Products
                .Select(p => new CartDetailsViewModel
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price
                })
                .ToList();

            return cartProducts;
        }

    }
}
