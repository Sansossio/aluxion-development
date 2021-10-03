using System.Collections.Generic;
using FileHandler.Models;

namespace FileHandler.Dto
{
  public class FileItemResponse
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }

    public static List<FileItemResponse> FromEntity (List<File> files) 
    {
      List<FileItemResponse> response = new List<FileItemResponse>();
      foreach (File file in files)
      {
        response.Add(FileItemResponse.FromEntity(file));
      }
      return response;
    }

    public static FileItemResponse FromEntity(File data)
    {
      return new FileItemResponse
      {
        ID = data.ID,
        Name = data.Name,
        Path = data.Path
      };
    }
  }
}
