using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Contracts;
using FootballManager.ViewModels;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        public PlayersController(
            Request request,
            IPlayerService _playerService)
            : base(request)
        {
            this.playerService = _playerService;
        }

        [Authorize]
        public Response All()
        {
            var allPlayers = playerService.GetAllPlayers();

            return View(new
            {
                IsAuthenticated = true,
                allPlayers
            });
        }

        [Authorize]
        public Response Add()
        {
            return View(new { IsAuthenticated = true });
        }

        [Authorize]
        [HttpPost]
        public Response Add(AddPlayerFormModel playerModel)
        {
            var (isCreated, error) = playerService.CreatePlayer(playerModel, User.Id);

            if (isCreated == false)
            {
                return View(error, "/Error");
            }   

            return Redirect("/Players/All");
        }

        [Authorize]
        public Response Collection()
        {
            var myPlayers = playerService.GetMyPlayers(User.Id);

            return View(myPlayers);
        }

        [Authorize]
        public Response AddToCollection(int playerId)
        {
            var(isAdded, error) =  playerService.AddToCollection(User.Id, playerId);

            if(isAdded == false && error == "Player exists in your collection!")
            {
                return Redirect("/Players/All");
            }

            if(isAdded == false)
            {
                return View(error, "/Error");
            }

            return Redirect("/Players/All");
        }


        [Authorize]
        public Response RemoveFromCollection(int playerId)
        {
            var (isRemoved, error) = playerService.RemoveFromCollection(User.Id, playerId);

            if (isRemoved == false)
            {
                return View(error, "/Error");
            }

            return Redirect("/Players/Collection");
        }


    }
}
