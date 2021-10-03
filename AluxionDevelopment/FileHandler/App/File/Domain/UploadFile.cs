using System.ComponentModel.DataAnnotations;

namespace FileHandler.Dto
{
  public class UploadFileDto
  {
    [Required]
    [Url]
    public string FileUrl { get; set; }
  }
}
