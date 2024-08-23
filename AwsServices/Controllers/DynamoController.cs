using DynamoService.Domain;
using DynamoService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AwsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamoController : ControllerBase
    {
        private readonly IDynamoService dynamoService;

        public DynamoController(IDynamoService dynamoService)
        {
            this.dynamoService = dynamoService;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {

            try
            {
                var tables = await this.dynamoService.GetAllAsync();
                return Ok(tables);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {

            try
            {
                Customer customer = await this.dynamoService.GetAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(Customer customer)
        {

            try
            {
                bool res = await this.dynamoService.CreateAsync(customer);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Customer customer)
        {

            try
            {
                bool res = await this.dynamoService.UpdateAsync(customer);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {

            try
            {
                bool res = await this.dynamoService.DeleteAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
