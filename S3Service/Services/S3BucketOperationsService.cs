using Amazon.S3;
using Amazon.S3.Model;
using S3Service.Interfces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Service.Services
{
    public class S3BucketOperationsService : IS3BucketOperationsService
    {
        private readonly IAmazonS3 amazonS3Client;
        public S3BucketOperationsService(IAmazonS3 amazonS3Client)
        {
            this.amazonS3Client = amazonS3Client;
        }
        public async Task<bool> BucketExist(string bucketName)
        {
            bool bucketExist = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(this.amazonS3Client, bucketName);
            return bucketExist;
        }

        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            bool bucketExist = await BucketExist(bucketName);
            if (bucketExist) return false;
            await this.amazonS3Client.PutBucketAsync(bucketName);
            return true;
        }

        public async Task<bool> DeleteBucketsAsync(string bucketName)
        {
            await this.amazonS3Client.DeleteBucketAsync(bucketName);
            return true;
        }

        public async Task<List<string>> GetBucketsAsync()
        {
            ListBucketsResponse data = await this.amazonS3Client.ListBucketsAsync();
            List<string> buckets = data.Buckets.Select(b => { return b.BucketName; }).ToList();
            return buckets;
        }
    }
}
