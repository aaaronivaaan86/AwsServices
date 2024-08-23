using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using S3Service.Interfces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Service.Services
{
    public class S3FileOperationsService : IS3FileOperationsService
    {
        private readonly IAmazonS3 amazonS3Client;
        private readonly IS3BucketOperationsService s3BucketOperationsService;

        public S3FileOperationsService(IAmazonS3 amazonS3Client, IS3BucketOperationsService s3BucketOperationsService)
        {
            this.amazonS3Client = amazonS3Client;
            this.s3BucketOperationsService = s3BucketOperationsService;
        }

        public async Task<bool> DeleteFileAsync(string bucketName, string? key)
        {
            bool bucketExist = await this.s3BucketOperationsService.BucketExist(bucketName);
            if (!bucketExist) return false;

            var res = await this.amazonS3Client.DeleteObjectAsync(bucketName, key);
            return true;

        }

        public async Task<List<GetPreSignedUrlRequest>> GetFilesAsync(string bucketName, string? prefix)
        {
            bool bucketExist = await this.s3BucketOperationsService.BucketExist(bucketName);
            if (!bucketExist) return new List<GetPreSignedUrlRequest>();

            var req = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };

            var res = await this.amazonS3Client.ListObjectsV2Async(req);

            List<GetPreSignedUrlRequest> s3Objects = res.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest() { BucketName = s.BucketName, Key = s.Key, Expires = DateTime.UtcNow.AddHours(3) };
                return urlRequest;
            }).ToList();


            return s3Objects;
        }

        public async Task<GetObjectResponse> GetFilesByKeyAsync(string bucketName, string? key)
        {
            bool bucketExist = await this.s3BucketOperationsService.BucketExist(bucketName);
            if (!bucketExist) return new GetObjectResponse();

            GetObjectResponse res = await this.amazonS3Client.GetObjectAsync(bucketName, key);
            return res;
        }

        public async Task<PutObjectResponse> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix)
        {
            bool bucketExist = await this.s3BucketOperationsService.BucketExist(bucketName);
            if (!bucketExist) return new PutObjectResponse();

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? formFile.FileName : $"{prefix?.TrimEnd('/')}/{formFile.FileName}",
                InputStream = formFile.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", formFile.ContentType);
            return await this.amazonS3Client.PutObjectAsync(request);
        }
    }
}
