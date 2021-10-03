using System.ComponentModel.DataAnnotations;

namespace FileHandler.Dto
{
  public class RegisterUser
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }

  public class RegisterUserResponse 
  {
    public bool Success { get; set; }
  }
}
