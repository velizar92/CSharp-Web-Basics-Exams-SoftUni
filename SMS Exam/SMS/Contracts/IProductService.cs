using SMS.Models;
using System.Collections.Generic;

namespace SMS.Contracts
{
    public interface IProductService
    {
        (bool, string) CreateProduct(CreateProductFormModel productModel);

        IEnumerable<IndexLoginViewModel> GetAllProducts(string userId);
    }
}
