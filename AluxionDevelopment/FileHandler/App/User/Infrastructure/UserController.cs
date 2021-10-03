using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileHandler.Models;
using FileHandler.Services;
using FileHandler.Dto;

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

    [HttpPost("forgot-password")]
    public ForgotPasswordResponse ForgotPassword([FromBody] ForgotPassword data)
    {
      return this.service.ForgotPassword(data);
    }

    [HttpPost("forgot-password/set-new-password")]
    public LoginUserResponse SetNewPassword([FromBody] SetNewPassword data)
    {
      return this.service.SetNewPassword(data);
    }

    [HttpPost("register")]
    public RegisterUserResponse Register([FromBody] RegisterUser data)
    {
      return this.service.Register(data);
    }

    [HttpPost("login")]
    public LoginUserResponse Login([FromBody] LoginUser data)
    {
      return this.service.Login(data);
    }
  }
}
