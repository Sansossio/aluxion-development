using System;
using System.Collections.Generic;

namespace FileHandler.Models
{
  public class User
  {
    public int ID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual ICollection<File> Files { get; set; }
  }
}
