using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileHandler.Dto;

namespace FileHandler.Services {
  public class ImagesService {
    private readonly UnsplashService unsplashService;

    public ImagesService (UnsplashService unsplashService) 
    {
      this.unsplashService = unsplashService;
    }

    public async Task<List<ExternalImage>> Search (string query)
    {
      var values = await this.unsplashService.SearchImages(query);
      return ExternalImage.FromResult(values);
    }
  }
}