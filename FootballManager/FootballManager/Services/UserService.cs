using System.Text;
using FootballManager.Contracts;
using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels;
using System.Security.Cryptography;

namespace FootballManager.Services
{
    public class UserService : IUserService
    {
        private readonly FootballManagerDbContext dbContext;
        private readonly IValidationService validationService;


        public UserService(FootballManagerDbContext _dbContext, IValidationService _validationService)
        {
            this.dbContext = _dbContext;
            this.validationService = _validationService;
        }


        public string GetUsername(string userId)
        {
            return dbContext.Users
                .FirstOrDefault(u => u.Id == userId)?.Username;
        }


        public string Login(LoginViewModel model)
        {
            var user = dbContext.Users
                .Where(u => u.Username == model.Username)
                .Where(u => u.Password == CalculateHash(model.Password))
                .SingleOrDefault();

            return user?.Id;
        }


        public (bool registered, string error) Register(RegisterViewModel model)
        {
            bool registered = false;
            string error = null;

            var (isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }
         
            User user = new User()
            {
                Email = model.Email,
                Password = CalculateHash(model.Password),
                Username = model.Username,               
            };

            try
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                registered = true;
            }
            catch (Exception)
            {
                error = "Could not save user in DB";
            }

            return (registered, error);
        }



        private string CalculateHash(string password)
        {
            byte[] passworArray = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(passworArray));
            }
        }


    }
}
