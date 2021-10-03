using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileHandler.Models;

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
  }
}
