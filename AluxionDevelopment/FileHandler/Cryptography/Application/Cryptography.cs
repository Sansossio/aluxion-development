using System;
using System.Security.Cryptography;

namespace FileHandler.Cryptography
{
  public class Crypto
  {
    private static readonly string key = Environment.GetEnvironmentVariable("CRYPTOGRAPHY_KEY");

    public static string Encrypt(string text)
    {
      var sha1 = SHA1.Create();
      var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"{key}{text}"));
      return Convert.ToBase64String(hash);
    }
  }
}
