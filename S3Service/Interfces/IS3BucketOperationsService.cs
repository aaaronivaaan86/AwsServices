using Amazon.S3.Model;

namespace S3Service.Interfces
{
    public interface IS3BucketOperationsService
    {
        public Task<List<string>> GetBucketsAsync();
        public Task<bool> CreateBucketAsync(string bucketName);
        public Task<bool> DeleteBucketsAsync(string bucketName);

        public Task<bool> BucketExist(string bucketName);
    }
}
