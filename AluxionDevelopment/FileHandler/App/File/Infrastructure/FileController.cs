using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileHandler.Models;
using Microsoft.AspNetCore.Authorization;
using FileHandler.Services;
using FileHandler.Dto;

namespace FileHandler.Controllers
{
  [ApiController]
  [Route("files")]
  [Authorize]
  public class FileController : ControllerBase
  {

    private readonly FileService service;
    private readonly UserService userService;

    public FileController(FileService service, UserService userService)
    {
      this.service = service;
      this.userService = userService;
    }

    [HttpGet("list")]
    public List<FileItemResponse> FilesList () {
      var user = this.userService.GetUserByClaim(this.User);
      return this.service.GetFilesByUser(user);
    }
  }
}
