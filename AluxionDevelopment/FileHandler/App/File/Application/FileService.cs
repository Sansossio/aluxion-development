using System;
using System.Collections.Generic;
using System.Linq;
using FileHandler.Models;
using FileHandler.Dto;

namespace FileHandler.Services
{
  public class FileService
  {
    private readonly DatabaseContext dbContext;

    public FileService(DatabaseContext context)
    {
      this.dbContext = context;
    }

    public List<FileItemResponse> GetFilesByUser(User user)
    {
      try {
        var files = this.dbContext.Files.Where(x => x.UserId == user.ID).ToList();
        return FileItemResponse.FromEntity(files);
      } catch (Exception e) {
        return new List<FileItemResponse>();
      }
    }
  }
}
