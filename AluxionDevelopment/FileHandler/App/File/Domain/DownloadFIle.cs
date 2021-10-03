using System.IO;
namespace FileHandler.Dto 
{
  public class DownloadFile {
    public Stream stream { get; set; }
    public string fileName { get; set; }
    public string contentType { get; set; }
  }
}