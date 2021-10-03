using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileHandler.Models;
using FileHandler.Services;

namespace FileHandler.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {

    private readonly UserService service;

    public UserController(UserService service)
    {
      this.service = service;
    }

    [HttpGet("list")]
    public List<User> UserList()
    {
      return this.service.UserList();
    }
  }
}
