using FootballManager.Contracts;
using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels;


namespace FootballManager.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FootballManagerDbContext dbContext;
        private readonly IValidationService validationService;

        public PlayerService(
            FootballManagerDbContext _dbContext,
            IValidationService _validationService)
        {
            this.dbContext = _dbContext;
            this.validationService = _validationService;
        }

        public (bool, string) AddToCollection(string userId, int playerId)
        {
            bool isAdded = false;
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var player = dbContext.Players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                return (isAdded, "Inavlid player.");
            }

            bool playerExists = dbContext.UserPlayers.Any(a => a.PlayerId == playerId && a.UserId == userId);

            if (playerExists)
            {
                return (isAdded, "Player exists in your collection!");
            }

            var userPlayer = new UserPlayer { Player = player, User = user };

            isAdded = true;
            dbContext.UserPlayers.Add(userPlayer);
            dbContext.SaveChanges();

            return (true, null);
        }


        public (bool, string) CreatePlayer(AddPlayerFormModel playerModel, string userId)
        {

            bool isCreated = false;

            var (isValid, error) = validationService.ValidateModel(playerModel);

            if (isValid == false)
            {
                return (isCreated, error);
            }

            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);

            Player player = new Player
            {
                FullName = playerModel.FullName,
                ImageUrl = playerModel.ImageUrl,
                Position = playerModel.Position,
                Speed = playerModel.Speed,
                Endurance = playerModel.Endurance,
                Description = playerModel.Description
            };

            dbContext.Players.Add(player);
            dbContext.SaveChanges();
            isCreated = true;

            return (isCreated, null);
        }



        public List<AllPlayersListingModel> GetAllPlayers()
        {
            var allPlayers =
                 dbContext.Players
                 .Select(p => new AllPlayersListingModel
                 {
                     Id = p.Id,
                     FullName = p.FullName,
                     ImageUrl = p.ImageUrl,
                     Position = p.Position,
                     Speed = p.Speed,
                     Endurance = p.Endurance,
                     Description = p.Description
                 })
                 .ToList();

            return allPlayers;
        }



        public List<AllPlayersListingModel> GetMyPlayers(string userId)
        {
            var myPlayers = new List<AllPlayersListingModel>();
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            

            if (user != null)
            {
                myPlayers = dbContext.UserPlayers
                    .Where(up => up.UserId == userId)
                    .Select(p => new AllPlayersListingModel
                    {
                        Id = p.Player.Id,
                        FullName = p.Player.FullName,
                        ImageUrl = p.Player.ImageUrl,
                        Position = p.Player.Position,
                        Speed = p.Player.Speed,
                        Endurance = p.Player.Endurance,
                        Description = p.Player.Description
                    })
                    .ToList();
            }

            return myPlayers;
        }



        public (bool, string) RemoveFromCollection(string userId, int playerId)
        {
            bool isRemoved = false;
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var player = dbContext.UserPlayers.FirstOrDefault(u => u.Player.Id == playerId);

            if (player == null)
            {
                return (isRemoved, "Inavlid player.");
            }

            var userPlayer = dbContext.UserPlayers.FirstOrDefault(f => f.PlayerId == playerId && f.UserId == userId);

            if (userPlayer == null)
            {
                return (isRemoved, $"This player not exist in {user.Username} collection");
            }

            isRemoved = true;
            user.UserPlayers.Remove(player);
            dbContext.SaveChanges();

            return (isRemoved, null);
        }
    }
}
