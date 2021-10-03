using System.ComponentModel.DataAnnotations;

namespace FileHandler.Dto 
{
  public class ForgotPassword 
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }

  public class ForgotPasswordResponse 
  {
    public bool Success { get; set; }
    public string Message { get; set; }
  }

  public class SetNewPassword {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string VerificationCode { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
  }
}
