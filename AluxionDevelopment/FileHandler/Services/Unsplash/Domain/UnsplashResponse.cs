using System;
using System.Collections.Generic;

namespace FileHandler.Dto
{
  public class UnsplashPhoto
  {
    public string id { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public UnsplashPhotoExif urls { get; set; }
  }

  public class UnsplashSearchResponse
  {
    public string total { get; set; }
    public string total_pages { get; set; }
    public List<UnsplashPhoto> results { get; set; }
  }
  public class UnsplashUserProfileImage
  {
    public string small { get; set; }
    public string medium { get; set; }
    public string large { get; set; }
  }

  public class UnsplashPhotoExif
  {
    public string raw { get; set; }
    public string make { get; set; }
    public string regular { get; set; }
    public string full { get; set; }
  }

}