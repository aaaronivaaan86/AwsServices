using AzBlobStorage.Interfces;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace AzBlobStorage.Servicces
{
    public class AzBlobStorageService : IAzBlobStorageService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient blobContainerClient;
        private readonly IConfiguration iConfig;

        public AzBlobStorageService(IConfiguration iConfig)
        {
            this.iConfig = iConfig;
            this.blobServiceClient = GetBlobServiceClient();
            this.blobContainerClient = CreateBlobContainerAsync("test-container-not-exist");
        }

        public BlobServiceClient GetBlobServiceClient()
        {
            string blobUri = this.iConfig.GetSection("AzConfiguration").GetSection("BlobStorageUri").Value;

            BlobServiceClient blobServiceClient = new BlobServiceClient(
                    new Uri(blobUri),
                    new DefaultAzureCredential()
                );

            return blobServiceClient;
        }


        public BlobContainerClient CreateBlobContainerAsync(string containerName)
        {
            var container =    this.blobServiceClient.GetBlobContainerClient(containerName);

            bool containerExist = container.Exists();
            if (containerExist)
            {
                return container;
            }

            BlobContainerClient blobContainerClient = this.blobServiceClient.CreateBlobContainer(containerName);
            return blobContainerClient;

        }


        public async void GetAllBlobsInContainer()
        {
            await foreach (BlobItem item in this.blobContainerClient.GetBlobsAsync())
            {
                await Console.Out.WriteLineAsync(item.Name);
            }
        }

        public (string, string) CreateLocalFile()
        {
            string localPath = "data";
            Directory.CreateDirectory(localPath);
            string fileName = "quickstart" + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);
            return (fileName , localFilePath);
        }

        public async Task<bool> UploadFile()
        {
            (string fileName, string localFilePath)  = CreateLocalFile();
            BlobClient blobClient = this.blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(localFilePath, true);
            return true;


        }


        public async Task<string> DownloadFile()
        {
            string localPath = "data";
            string fileName = "quickstart" + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);
            string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

            BlobClient blobClient = this.blobContainerClient.GetBlobClient(fileName);

            await blobClient.DownloadToAsync(downloadFilePath);
            return downloadFilePath;
        }

        public async Task<bool> DeleteBlob()
        {
            await this.blobContainerClient.DeleteAsync();
            return true;
        }

    }
}
