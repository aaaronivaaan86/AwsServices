using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Service.Interfces
{
    public interface IS3FileOperationsService
    {
        public Task<List<GetPreSignedUrlRequest>> GetFilesAsync(string bucketName, string? prefix);

        public  Task<GetObjectResponse> GetFilesByKeyAsync(string bucketName, string? key);
        public  Task<PutObjectResponse> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix);

        public Task<bool> DeleteFileAsync(string bucketName, string? key);

    }
}
