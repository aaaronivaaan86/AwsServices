using DynamoService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoService.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> CreateAsync(Customer customer);

        Task<Customer?> GetAsync(Guid id);

        Task<IEnumerable<Customer>> GetAllAsync();

        Task<bool> UpdateAsync(Customer customer);

        Task<bool> DeleteAsync(Guid id);
    }
}
