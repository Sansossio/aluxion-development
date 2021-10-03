using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileHandler.Models;
using FileHandler.Dto;
using FileHandler.Cryptography;
namespace FileHandler.Services
{
  public class UserService
  {
    private readonly DatabaseContext _context;
    public UserService(DatabaseContext context)
    {
      _context = context;
    }

    public List<User> UserList()
    {
      return _context.Users.ToList();
    }

    public RegisterUserResponse Register (RegisterUser data) {
      var exists = _context.Users.Where(x => x.Email == data.Email).Count() > 0;
      if (exists) {
        throw new Exception("User already exists");
      }
      var finalPassword = Cryptography.Crypto.Encrypt(data.Password);

      _context.Add(new User {
        Email = data.Email,
        Password = finalPassword
      });
      _context.SaveChanges();
      return new RegisterUserResponse {
        Success = true
      };
    }
  }
}
