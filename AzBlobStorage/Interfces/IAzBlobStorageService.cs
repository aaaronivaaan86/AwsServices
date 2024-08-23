using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBlobStorage.Interfces
{
    public interface IAzBlobStorageService
    {
        public BlobServiceClient GetBlobServiceClient();
        public BlobContainerClient CreateBlobContainerAsync(string containerName);
        public  void GetAllBlobsInContainer();
        public (string, string) CreateLocalFile();
        public Task<bool> UploadFile();
        public Task<string> DownloadFile();
        public Task<bool> DeleteBlob();
    }
}
