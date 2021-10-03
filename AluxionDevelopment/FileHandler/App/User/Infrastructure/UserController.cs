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

    /// <summary>
    /// Forgot password
    /// </summary>
    /// <param name="data"></param> 
    [HttpPost("forgot-password")]
    public ForgotPasswordResponse ForgotPassword([FromBody] ForgotPassword data)
    {
      return this.service.ForgotPassword(data);
    }

    /// <summary>
    /// Set a new password for the user
    /// </summary>
    /// <param name="data"></param> 
    [HttpPost("forgot-password/set-new-password")]
    public LoginUserResponse SetNewPassword([FromBody] SetNewPassword data)
    {
      return this.service.SetNewPassword(data);
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="data"></param> 
    [HttpPost("register")]
    public RegisterUserResponse Register([FromBody] RegisterUser data)
    {
      return this.service.Register(data);
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="data"></param> 
    [HttpPost("login")]
    public LoginUserResponse Login([FromBody] LoginUser data)
    {
      return this.service.Login(data);
    }
  }
}
