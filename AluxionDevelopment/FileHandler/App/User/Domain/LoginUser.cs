using System.ComponentModel.DataAnnotations;

namespace FileHandler.Dto
{
  public class LoginUser: RegisterUser
  {
  }

  public class LoginUserResponse
  {
    public string Token { get; set; }
  }
}
