using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FileHandler.Dto
{
  public class GetImagesQuery {
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
    public string query { get; set; }
  }
  public class ExternalImage
  {
    public string id { get; set; }
    public string url { get; set; }
    public string description { get; set; }

    public static ExternalImage FromResult (UnsplashPhoto result) 
    {
      return new ExternalImage {
        id = result.id,
        url = result.urls.raw,
        description = result.description
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