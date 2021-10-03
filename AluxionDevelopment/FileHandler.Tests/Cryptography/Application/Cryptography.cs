using System;
using Xunit;
using FileHandler.Cryptography;

namespace FileHandler.Tests
{
  public class Cryptography
  {
    [Fact]
    public void Should_Encrypt_Password()
    {
      Environment.SetEnvironmentVariable("CRYPTOGRAPHY_KEY", "test");
      Assert.NotEqual("test", Crypto.Encrypt("test"));
    }

    [Fact]
    public void Should_Change_Result_When_Env_Crypto_Variable_Change()
    {
      Environment.SetEnvironmentVariable("CRYPTOGRAPHY_KEY", "a");
      string value = "test";
      string encrypt1 = Crypto.Encrypt(value);
      Environment.SetEnvironmentVariable("CRYPTOGRAPHY_KEY", "b");
      string encrypt2 = Crypto.Encrypt(value);
      Assert.NotEqual(encrypt1, encrypt2);
    }
  }
}
