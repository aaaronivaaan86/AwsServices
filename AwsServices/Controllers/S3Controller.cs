using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3Service.Interfces;
using System.Collections.Generic;

namespace AwsServices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IS3BucketOperationsService bucketOperationsService;
        private readonly IS3FileOperationsService s3FileOperationsService;

        // TEST COMMENT
        public S3Controller(IS3BucketOperationsService bucketOperationsService, IS3FileOperationsService s3FileOperationsService)
        {
            this.bucketOperationsService = bucketOperationsService;
            this.s3FileOperationsService = s3FileOperationsService;
        }

    // comment

        #region BucketOperations
        [HttpGet("GetBucketsAsync")]
        public async Task<IActionResult> GetBucketsAsync()
        {

            try
            {
                var buckets = await this.bucketOperationsService.GetBucketsAsync();
                return Ok(buckets);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateBucketAsync")]
        public async Task<IActionResult> CreateBucketAsync(string bucketName)
        {
            try
            {
                bool res = await this.bucketOperationsService.CreateBucketAsync(bucketName);
                if (!res) return BadRequest("Bucker already exist");
                return Created("bucket", $"Bucket {bucketName} created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBucketsAsync")]
        public async Task<IActionResult> DeleteBucketsAsync(string bucketName)
        {
            try
            {
                bool res = await this.bucketOperationsService.DeleteBucketsAsync(bucketName);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        // Test Comment Nico
        #endregion

        #region FileOperation

        [HttpGet("GetFilesAsync")]
        public async Task<IActionResult> GetFilesAsync(string bucketName, string? prefix)
        {

            List<GetPreSignedUrlRequest> s3Objects = await this.s3FileOperationsService.GetFilesAsync(bucketName, prefix);


            return Ok(s3Objects);

        }

        [HttpGet("GetFilesByKeyAsync")]
        public async Task<IActionResult> GetFilesByKeyAsync(string bucketName, string? key)
        {


            var res = await this.s3FileOperationsService.GetFilesByKeyAsync(bucketName, key);
            return File(res.ResponseStream, res.Headers.ContentType);

        }


        [HttpPost("UploadFileAsync")]
        public async Task<IActionResult> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix)
        {
            PutObjectResponse putObjectResponse = await this.s3FileOperationsService.UploadFileAsync(formFile, bucketName, prefix);
            return Ok();

        }


        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> DeleteFileAsync(string bucketName, string? key)
        {
            bool putObjectResponse = await this.s3FileOperationsService.DeleteFileAsync(bucketName, key);
            return NotFound();

        }


        #endregion






    }
}
