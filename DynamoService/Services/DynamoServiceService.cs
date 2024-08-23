using DynamoService.Domain;
using DynamoService.Interfaces;


namespace DynamoService.Services
{
    public class DynamoServiceService : IDynamoService
    {
        private readonly ICustomerRepository customerRepository;
        public DynamoServiceService(ICustomerRepository customerRepository)
        {

            this.customerRepository = customerRepository;
        }

        public async Task<bool> CreateAsync(Customer customer)
        {
            return await this.customerRepository.CreateAsync(customer);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await this.customerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
           return await this.customerRepository.GetAllAsync();  
        }

        public async Task<Customer?> GetAsync(Guid id)
        {
            return await this.customerRepository.GetAsync(id);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            return await this.customerRepository.UpdateAsync(customer);
        }
    }
}
