using System.Net;
using System.Net.Http;
using System.IO;
using System;
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
  [Route("files")]
  [Authorize]
  public class FileController : ControllerBase
  {

    private readonly FileService service;
    private readonly UserService userService;
    private readonly S3 s3;

    public FileController(FileService service, UserService userService, S3 s3)
    {
      this.service = service;
      this.userService = userService;
      this.s3 = s3;
    }

    [HttpGet("list")]
    public List<FileItemResponse> FilesList()
    {
      var user = this.userService.GetUserByClaim(this.User);
      return this.service.GetFilesByUser(user);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public FileItemResponse GetById(int id)
    {
      return this.service.GetById(id);
    }

    [HttpGet("{id}/download")]
    [AllowAnonymous]
    public async Task DownloadById(int id)
    {
      var file = await this.service.DownloadById(id);
      Response.StatusCode = (int)HttpStatusCode.OK;
      Response.Headers.Add(HeaderNames.ContentDisposition, $"attachment; filename=\"{file.fileName}\"");
      Response.Headers.Add(HeaderNames.ContentType, file.contentType);
      await file.stream.CopyToAsync(Response.Body);
      await Response.Body.FlushAsync();
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public void DeleteFile(int id)
    {
      var user = this.userService.GetUserByClaim(this.User);
      this.service.DeleteById(id, user);
    }

    [HttpPost("upload")]
    public async ValueTask<FileItemResponse> UploadFileToS3(IFormFile file)
    {
      var user = this.userService.GetUserByClaim(this.User);
      return await this.service.UploadFile(file, user);
    }

    [HttpPost("upload/external")]
    public async ValueTask<FileItemResponse> UploadExternalFileToS3(UploadFileDto file)
    {
      var user = this.userService.GetUserByClaim(this.User);
      return await this.service.UploadExternalFile(file, user);
    }
  }
}
