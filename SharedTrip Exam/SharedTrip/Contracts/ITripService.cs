using SharedTrip.Models;
using System.Collections.Generic;


namespace SharedTrip.Contracts
{
    public interface ITripService
    {

        DetailsTripModel GetTripDetails(string _tripId);

        IEnumerable<AllTripsListingModel> GetAllTrips();

        bool CheckIfUserIsJoinedToTrip(string _tripId, string _userId);

        (bool, IEnumerable<ErrorViewModel>) AddTrip(AddTripFormModel _formModel);

        (bool, IEnumerable<ErrorViewModel>) AddUserToTrip(string _tripId, string _userId);
    }
}
