using System.Linq;
using SharedTrip.Models;
using SharedTrip.Contracts;
using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService;

        public TripsController(
            Request request,
            ITripService _tripService)
            : base(request)
        {
            this.tripService = _tripService;
        }


        [Authorize]
        [HttpGet]
        public Response All()
        {
            var trips = tripService.GetAllTrips();

            return View(new
            {
                trips,
                IsAuthenticated = true
            });
        }


        [Authorize]
        [HttpGet]
        public Response Add()
        {
            return View(new { IsAuthenticated = true });
        }


        [Authorize]
        [HttpPost]
        public Response Add(AddTripFormModel formModel)
        {
            var (isAdded, errors) = tripService.AddTrip(formModel);
            if (isAdded == true)
            {
                return Redirect("/Trips/All");
            }

            return View(errors, "/Error");
        }


        [Authorize]
        [HttpGet]
        public Response Details(string tripId)
        {
            DetailsTripModel tripDetails = tripService.GetTripDetails(tripId);

            return View(tripDetails);    
        }



        [Authorize]
        public Response AddUserToTrip(string tripId)
        {
            var (isAdded, errors) = tripService.AddUserToTrip(tripId, User.Id);

            if(isAdded == false && errors.Count() > 0)
            {
                if(errors.Count() > 0)
                {
                    return View(errors, "/Error");
                }
                if (errors.Count() == 0)
                {
                    return Redirect($"/Trips/Details/?tripId={tripId}");
                }
            }

            return Redirect("/Trips/All");
        }


    }
}
