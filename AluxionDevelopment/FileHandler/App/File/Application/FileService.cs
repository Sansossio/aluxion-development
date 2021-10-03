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
  }
}
