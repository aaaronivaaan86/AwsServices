using DynamoService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoService.Interfaces
{
    public interface IDynamoService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetAsync(Guid id);
        Task<bool> CreateAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid id);
    }
}
