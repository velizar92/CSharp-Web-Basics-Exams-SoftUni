using SharedTrip.Contracts;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IValidationService validationService;
       
        public UserService(
            ApplicationDbContext _dbContext,
            IValidationService _validationService)
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

        public (bool registered, IEnumerable<ErrorViewModel> errors) Register(RegisterViewModel model)
        {
            bool registered = false;
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

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
                errors.Add(new ErrorViewModel("Could not save user in DB"));
            }

            return (registered, errors);
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
