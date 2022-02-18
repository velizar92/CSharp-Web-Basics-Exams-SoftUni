using SMS.Contracts;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services
{
    public class ProductService : IProductService
    {
        private readonly SMSDbContext dbContext;
        private readonly IValidationService validationService;
        


        public ProductService(
            SMSDbContext _dbContext,
            IValidationService _validationService)
        {
            this.dbContext = _dbContext;
            this.validationService = _validationService;         
        }


        public (bool, string) CreateProduct(CreateProductFormModel productModel)
        {
            bool isCreated = false;            

            var (isValid, error) = validationService.ValidateModel(productModel);

            if(isValid == false)
            {
                return (isCreated, error);
            }

            Product product = new Product
            {
                Name = productModel.Name,
                Price = productModel.Price
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            isCreated = true;

            return (isCreated, null);
        }


        public IEnumerable<IndexLoginViewModel> GetAllProducts(string userId)
        {           
            var products = dbContext.Products
                  .Select(p => new IndexLoginViewModel
                  {                    
                      ProductId = p.Id,
                      ProductName = p.Name,
                      ProductPrice = p.Price,
                  })
                  .ToList();
            
            return products;
        }
    }
}
