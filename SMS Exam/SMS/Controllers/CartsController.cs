using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {

        private readonly ICartService cartService;

        public CartsController(
            Request request,
            ICartService _cartService)
            : base(request)
        {
            this.cartService = _cartService;
        }


        [HttpGet]
        public Response Details()
        {
            var products = cartService.GetCartProducts(User.Id);

            return View(products);
        }


        [Authorize]
        public Response AddProduct(string productId)
        {
            var (isAdded, error) = cartService.AddProductToCart(productId, User.Id);

            if (!isAdded)
            {
                return View(error, "/Error");
            }

            var products = cartService.GetCartProducts(User.Id);

            return View(new
            {
                products,
                IsAuthenticated = true
            }, "/Carts/Details");
        }


        [Authorize]
        public Response Buy()
        {
           var (isCleaned, error) = cartService.CleanCart(User.Id);

            if(!isCleaned)
            {
                return View(error, "/Error");
            }

            return Redirect("/Home/Index");
        }


    }
}
