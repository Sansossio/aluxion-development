namespace FileHandler.Models
{
  public class File
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
  }
}
