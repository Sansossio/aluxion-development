using System.IO;
using System;
using Amazon.S3;
using System.Threading.Tasks;
using Amazon.S3.Model;

namespace FileHandler.Services
{
  public class S3
  {
    private readonly string bucket = Environment.GetEnvironmentVariable("AWS_BUCKET");
    private readonly string bucketPrefix = Environment.GetEnvironmentVariable("AWS_BUCKET_PREFIX");
    private readonly string region = Environment.GetEnvironmentVariable("AWS_REGION");

    private readonly AmazonS3Client s3Client = new AmazonS3Client(
      Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"),
      Environment.GetEnvironmentVariable("AWS_SECRET_KEY"));

    public string GetFileUrl(string fileName)
    {
      return $"http://{bucket}.s3.{region}.amazonaws.com/{fileName}";
    }

    public async Task<string> Upload(string fileName, MemoryStream data)
    {
      fileName = $"{bucketPrefix}/{fileName}";
      var item = await this.s3Client.PutObjectAsync(new PutObjectRequest
      {
        BucketName = this.bucket,
        Key = fileName,
        InputStream = data,
        CannedACL = S3CannedACL.PublicRead
      });
      return fileName;
    }

    public async Task<GetObjectResponse> Download (string path)
    {
      var request = new GetObjectRequest
      {
        BucketName = this.bucket,
        Key = path
      };
      return await this.s3Client.GetObjectAsync(request);
    }
  }
}
