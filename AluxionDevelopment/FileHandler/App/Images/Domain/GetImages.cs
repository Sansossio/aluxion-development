using System;
using System.Collections.Generic;

namespace FileHandler.Dto
{
  public class ExternalImage
  {
    public string id { get; set; }
    public string url { get; set; }

    public static ExternalImage FromResult (UnsplashPhoto result) 
    {
      return new ExternalImage {
        id = result.id,
        url = result.urls.raw
      };
    }

    public static List<ExternalImage> FromResult (UnsplashSearchResponse result) 
    {
      var response = new List<ExternalImage>();
      foreach (var item in result.results) 
      {
        response.Add(ExternalImage.FromResult(item));
      }
      return response;
    }
  }
}