using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileHandler.Models;
using FileHandler.Dto;
using FileHandler.Auth;
namespace FileHandler.Services
{
  public class UserService
  {
    private readonly DatabaseContext dbContext;
    private readonly JwtAuthenticationService authService;

    public UserService(DatabaseContext context, JwtAuthenticationService authService)
    {
      this.dbContext = context;
      this.authService = authService;
    }

    public List<User> UserList()
    {
      return dbContext.Users.ToList();
    }

    public RegisterUserResponse Register (RegisterUser data) {
      var exists = dbContext.Users.Where(x => x.Email == data.Email).Count() > 0;
      if (exists) {
        throw new Exception("User already exists");
      }
      var finalPassword = Cryptography.Crypto.Encrypt(data.Password);

      dbContext.Add(new User {
        Email = data.Email,
        Password = finalPassword
      });
      dbContext.SaveChanges();

      return new RegisterUserResponse {
        Success = true
      };
    }
  
    public LoginUserResponse Login (LoginUser data) {
      var finalPassword = Cryptography.Crypto.Encrypt(data.Password);
      var user = dbContext.Users.Where(x => x.Email == data.Email && x.Password == finalPassword).First();
      if (user == null) {
        throw new Exception("User does not exist");
      }
      return new LoginUserResponse {
        Token = $"Bearer {this.authService.Authenticate(user.Email)}"
      };
    }

    public User GetUserByClaim (ClaimsPrincipal currentUser) {
      var email = currentUser.FindFirst(ClaimTypes.Email).Value;
      return dbContext.Users.Where(x => x.Email == email).First();
    }
  }
}
