using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoService.Domain
{
    public static class CustomerRequestToCustomerMapping
    {
        public static Customer ToCustomer(this CustomerRequest customerRequest)
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                GitHubUsername = customerRequest.GitHubUsername,
                DateOfBirth = customerRequest.DateOfBirth,
                FullName = customerRequest.FullName,
                Email = customerRequest.Email,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
