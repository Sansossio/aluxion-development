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

    public RegisterUserResponse Register(RegisterUser data)
    {
      var exists = dbContext.Users.Where(x => x.Email == data.Email).Count() > 0;
      if (exists)
      {
        throw new Exception("User already exists");
      }
      var finalPassword = Cryptography.Crypto.Encrypt(data.Password);

      dbContext.Add(new User
      {
        Email = data.Email,
        Password = finalPassword
      });
      dbContext.SaveChanges();

      return new RegisterUserResponse
      {
        Success = true
      };
    }

    public LoginUserResponse Login(LoginUser data)
    {
      var finalPassword = Cryptography.Crypto.Encrypt(data.Password);
      var user = dbContext.Users.Where(x => x.Email == data.Email && x.Password == finalPassword).FirstOrDefault();
      if (user == null)
      {
        throw new Exception("Email or password are invalid");
      }
      return new LoginUserResponse
      {
        Token = $"Bearer {this.authService.Authenticate(user.Email)}"
      };
    }

    public User GetUserByClaim(ClaimsPrincipal currentUser)
    {
      var email = currentUser.FindFirst(ClaimTypes.Email).Value;
      return dbContext.Users.Where(x => x.Email == email).FirstOrDefault();
    }

    public ForgotPasswordResponse ForgotPassword(ForgotPassword data)
    {
      var user = dbContext.Users.Where(x => x.Email == data.Email).FirstOrDefault();
      if (user == null)
      {
        throw new Exception("User does not exist");
      }
      var verificationCode = Guid.NewGuid().ToString();
      user.VerificationCode = verificationCode;

      string subject = "Reset Password";
      string body = $"Verification code to restart your password is: {verificationCode}";

      MailService.Send(new SendMail
      {
        To = data.Email,
        Subject = subject,
        Body = body
      });

      dbContext.SaveChanges();

      return new ForgotPasswordResponse
      {
        Success = true,
        Message = "Verification code sent, check your email (or smtp mock if you are in local)"
      };
    }

    public LoginUserResponse SetNewPassword(SetNewPassword data)
    {
      var user = dbContext.Users.Where(x => x.Email == data.Email).FirstOrDefault();
      if (user == null)
      {
        throw new Exception("User does not exist");
      }
      if (user.VerificationCode != data.VerificationCode)
      {
        throw new Exception("Verification code is not valid");
      }
      var finalPassword = Cryptography.Crypto.Encrypt(data.NewPassword);
      user.Password = finalPassword;
      user.VerificationCode = null;

      dbContext.SaveChanges();

      return this.Login(new LoginUser
      {
        Email = data.Email,
        Password = data.NewPassword
      });
    }
  }
}
