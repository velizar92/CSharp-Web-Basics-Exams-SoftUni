using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;
using SMS.Models;


namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(
            Request request,
            IProductService _productService) 
            : base(request)
        {
            this.productService = _productService;
        }


        [Authorize]
        [HttpGet]
        public Response Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public Response Create(CreateProductFormModel productModel)
        {
            var (isCreated, error) = productService.CreateProduct(productModel);

            if(isCreated == false)
            {
                return View(error, "/Error");
            }

            return Redirect("/");
        }


    }
}
