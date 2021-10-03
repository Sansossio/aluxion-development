using System;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FileHandler.Dto;

namespace FileHandler.Services
{
  public class UnsplashService
  {
    private readonly string clientId = Environment.GetEnvironmentVariable("UNSPLASH_CLIENT_ID");
    private readonly string baseUrl = "https://api.unsplash.com";

    async Task<UnsplashSearchResponse> request(string path, string query)
    {
      var client = new RestClient(this.baseUrl);
      var request = new RestRequest(path, Method.GET);
      request
        .AddParameter("client_id", this.clientId)
        .AddParameter("query", query);
      IRestResponse response = await client.ExecuteAsync(request);

      return JsonConvert.DeserializeObject<UnsplashSearchResponse>(response.Content);
    }

    public async Task<UnsplashSearchResponse> SearchImages(string query)
    {
      return await this.request("search/photos", query);
    }
  }
}
