using System;
using System.Collections.Generic;
using System.Linq;
using FileHandler.Models;
using FileHandler.Dto;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RestSharp;

namespace FileHandler.Services
{
  public class FileService
  {
    private readonly DatabaseContext dbContext;
    private readonly S3 s3;


    public FileService(DatabaseContext context, S3 s3)
    {
      this.dbContext = context;
      this.s3 = s3;
    }

    public List<FileItemResponse> GetFilesByUser(User user)
    {
      var files = this.dbContext.Files.Where(x => x.UserId == user.ID).ToList();
      return FileItemResponse.FromEntity(files);
    }

    public void DeleteById(int id, User user)
    {
      var file = this.dbContext.Files.FirstOrDefault(x => x.ID == id && x.UserId == user.ID);
      if (file != null)
      {
        this.dbContext.Files.Remove(file);
        this.dbContext.SaveChanges();
      }
    }

    public FileItemResponse GetById(int id)
    {
      var file = this.dbContext.Files.FirstOrDefault(x => x.ID == id);
      if (file == null)
      {
        throw new Exception("File not found");
      }
      return FileItemResponse.FromEntity(file);
    }

    public FileItemResponse UpdateName(int id, string name, User user)
    {
      var file = this.dbContext.Files.FirstOrDefault(x => x.ID == id && x.UserId == user.ID);
      if (file == null || file.UserId != user.ID)
      {
        return null;
      }

      file.Name = name;
      this.dbContext.SaveChanges();

      return this.GetById(id);
    }

    public async ValueTask<FileItemResponse> UploadFile(IFormFile file, User user)
    {
      var memoryStream = new System.IO.MemoryStream();
      file.CopyTo(memoryStream);

      string fileName = $"{Guid.NewGuid()}-{file.FileName}";
      string filePath = await s3.Upload(fileName, memoryStream);
      File newFile = new File
      {
        Name = file.FileName,
        Path = filePath,
        UserId = user.ID
      };
      var newnewfile = this.dbContext.Files.Add(newFile);
      this.dbContext.SaveChanges();

      return this.GetById(newFile.ID);
    }

    public async ValueTask<FileItemResponse> UploadExternalFile(UploadFileDto file, User user)
    {
      var client = new RestClient(file.FileUrl);
      var request = new RestRequest(Method.GET);
      var fileData = client.DownloadData(request);
      var filename = file.FileUrl.Split('/').Last();
      var memoryStream = new System.IO.MemoryStream(fileData);

      string filePath = await s3.Upload($"{Guid.NewGuid()}", memoryStream);
      File newFile = new File
      {
        Name = filename,
        Path = filePath,
        UserId = user.ID
      };
      var newnewfile = this.dbContext.Files.Add(newFile);
      this.dbContext.SaveChanges();

      return this.GetById(newFile.ID);
    }

    public async Task<DownloadFile> DownloadById(int id)
    {
      var file = this.dbContext.Files.FirstOrDefault(x => x.ID == id);
      if (file == null)
      {
        throw new Exception("File not found");
      }
      var fileFromS3 = await this.s3.Download(file.Path);

      return new DownloadFile
      {
        stream = fileFromS3.ResponseStream,
        fileName = file.Name,
        contentType = fileFromS3.Headers.ContentType
      };
    }
  }
}
