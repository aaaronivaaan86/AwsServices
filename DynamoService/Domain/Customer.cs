using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DynamoService.Domain
{
    public class Customer
    {
        [JsonPropertyName("im")]
        public string Im => Id.ToString();

        [JsonPropertyName("mi")]
        public string Mi => Id.ToString();

        public Guid Id { get; init; } = default!;

        public string GitHubUsername { get; init; } = default!;

        public string FullName { get; init; } = default!;

        public string Email { get; init; } = default!;

        public DateTime DateOfBirth { get; init; }

        public DateTime UpdatedAt { get; set; }
    }
}
