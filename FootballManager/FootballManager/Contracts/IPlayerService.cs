using FootballManager.ViewModels;

namespace FootballManager.Contracts
{
    public interface IPlayerService
    {
        (bool, string) CreatePlayer(AddPlayerFormModel playerModel, string _userId);

        List<AllPlayersListingModel> GetAllPlayers();

        List<AllPlayersListingModel> GetMyPlayers(string userId);

        (bool, string) AddToCollection(string userId, int playerId);

        (bool, string) RemoveFromCollection(string userId, int playerId);

    }
}
