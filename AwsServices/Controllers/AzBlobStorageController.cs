using AzBlobStorage.Interfces;
using Microsoft.AspNetCore.Mvc;

namespace AwsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzBlobStorageController : ControllerBase
    {
        private readonly IAzBlobStorageService _blobStorageService;

        public AzBlobStorageController(IAzBlobStorageService azBlobStorageService)
        {
            _blobStorageService = azBlobStorageService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> ListBlobs()
        {
            try
            {
                _blobStorageService.GetAllBlobsInContainer();
                return Ok("Blob listing complete.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                bool result = await _blobStorageService.UploadFile();
                return result ? Ok("File uploaded successfully.") : BadRequest("File upload failed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile()
        {
            try
            {
                string filePath = await _blobStorageService.DownloadFile();
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var fileName = System.IO.Path.GetFileName(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteContainer()
        {
            try
            {
                bool result = await _blobStorageService.DeleteBlob();
                return result ? Ok("Blob container deleted successfully.") : BadRequest("Blob container deletion failed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
