using System;
using FileHandler.Models;
using Microsoft.EntityFrameworkCore;

namespace FileHandler
{
  public class DatabaseContext : DbContext
  {

    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      string host = Environment.GetEnvironmentVariable("DATABASE_HOST");
      string database = Environment.GetEnvironmentVariable("DATABASE_DB");
      string user = Environment.GetEnvironmentVariable("DATABASE_USER");
      string password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

      optionsBuilder
        .UseNpgsql($"Host={host};Database={database};Username={user};Password={password}");
    }
  }
}
