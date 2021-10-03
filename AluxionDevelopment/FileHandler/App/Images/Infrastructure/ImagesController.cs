using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FileHandler.Services;
using FileHandler.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace FileHandler.Controllers
{
  [ApiController]
  [Route("images")]
  [Authorize]
  public class ImagesController : ControllerBase
  {

    private readonly ImagesService service;

    public ImagesController(ImagesService service)
    {
      this.service = service;
    }

    /// <summary>
    /// Search an image on Unsplash
    /// </summary>
    /// <param name="data"></param> 
    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<List<ExternalImage>> Search ([FromQuery] GetImagesQuery data)
    {
      return await this.service.Search(data.query);
    }
  }
}