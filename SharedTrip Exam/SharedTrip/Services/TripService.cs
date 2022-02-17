using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using SharedTrip.Contracts;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;


namespace SharedTrip.Services
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidationService validationService;

        public TripService(
            ApplicationDbContext _dbContext,
            IValidationService _validationService)
        {
            this.dbContext = _dbContext;
            this.validationService = _validationService;
        }


        public (bool, IEnumerable<ErrorViewModel>) AddTrip(AddTripFormModel _formModel)
        {
            var (isValid, errors) = validationService.ValidateModel(_formModel);
            bool isAdded = false;

            if (isValid == false)
            {
                return (isAdded, errors);
            }

            DateTime date;

            DateTime.TryParseExact(
                _formModel.DepartureTime,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            var trip = new Trip
            {
                StartPoint = _formModel.StartPoint,
                EndPoint = _formModel.EndPoint,
                DepartureTime = date,
                ImagePath = _formModel.ImagePath,
                Seats = _formModel.Seats,
                Description = _formModel.Description,
            };

            dbContext.Add(trip);
            dbContext.SaveChanges();

            isAdded = true;
            return (isAdded, errors);
        }



        public IEnumerable<AllTripsListingModel> GetAllTrips()
        {
            var trips = dbContext.Trips
                 .Select(t => new AllTripsListingModel
                 {
                     Id = t.Id,
                     StartPoint = t.StartPoint,
                     EndPoint = t.EndPoint,
                     DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                     Seats = t.Seats
                 })
                 .ToList();

            return trips;
        }



        public (bool, IEnumerable<ErrorViewModel>) AddUserToTrip(string _tripId, string _userId)
        {
            bool isAdded = false;
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            var user = dbContext.Users.FirstOrDefault(u => u.Id == _userId);
            var trip = dbContext.Trips.FirstOrDefault(t => t.Id == _tripId);

            if (user == null)
            {
                errors.Add(new ErrorViewModel("User with provided 'Id' does not exists."));
            }

            if (trip == null)
            {
                errors.Add(new ErrorViewModel("Trip with provided 'Id' does not exists."));
            }

            if (CheckIfUserIsJoinedToTrip(_tripId, _userId) == true)
            {
                isAdded = false;
                return (isAdded, errors);
            }
            dbContext.UserTrips.Add(
                new UserTrip()
                {
                    User = user,
                    Trip = trip
                });

            dbContext.SaveChanges();
            isAdded = true;

            return (isAdded, errors);
        }



        public DetailsTripModel GetTripDetails(string _tripId)
        {
            var trip = dbContext.Trips.FirstOrDefault(t => t.Id == _tripId);

            return new DetailsTripModel
            {
                Id = _tripId,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Description = trip.Description,
                Seats = trip.Seats,
            };
        }


        public bool CheckIfUserIsJoinedToTrip(string _tripId, string _userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == _userId);
            var trip = dbContext.Trips.FirstOrDefault(t => t.Id == _tripId);

            if (dbContext.UserTrips
              .FirstOrDefault(ut => ut.User == user && ut.Trip == trip) != null)
            {
                return true;
            }

            return false;
        }

    }
}
