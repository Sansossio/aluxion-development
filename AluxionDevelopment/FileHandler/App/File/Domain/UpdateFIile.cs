using System.ComponentModel.DataAnnotations;

namespace FileHandler.Dto 
{
  public class UpdateFileName {
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; }
  }
}