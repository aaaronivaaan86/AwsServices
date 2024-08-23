using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DynamoService.Domain;
using DynamoService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DynamoService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IAmazonDynamoDB dynamoDB;
        private readonly string _tableName = "IMTable";


        public CustomerRepository(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }

        public async Task<bool> CreateAsync(Customer customer)
        {
            customer.UpdatedAt = DateTime.UtcNow;
            string  customerAsJson = JsonSerializer.Serialize(customer);
            Dictionary<string, AttributeValue> customerAsItem = Document.FromJson(customerAsJson).ToAttributeMap();

            var createCutomerRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = customerAsItem
            };

            var  res = await this.dynamoDB.PutItemAsync(createCutomerRequest);
            return res.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleteRequest = new DeleteItemRequest { 
                TableName = _tableName,
                Key =
                {
                    {"im", new AttributeValue{S = id.ToString() }},
                    {"mi", new AttributeValue{S = id.ToString() }}
                }
            };

            var res = await this.dynamoDB.DeleteItemAsync(deleteRequest);

            return res.HttpStatusCode == System.Net.HttpStatusCode.OK;

        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            ScanRequest scanRequest = new ScanRequest { TableName = _tableName};

            var res = await dynamoDB.ScanAsync(scanRequest);
            var items = res.Items.Select(x =>
            {
                var json = Document.FromAttributeMap(x).ToJson();
                return JsonSerializer.Deserialize<Customer>(json);
            });

            return items;

        }

        public async Task<Customer?> GetAsync(Guid id)
        {
            var getItemReq = new GetItemRequest
            {
                TableName = _tableName,
                Key =
                {
                    {"im", new AttributeValue{S = id.ToString() }},
                    {"mi", new AttributeValue{S = id.ToString() }}
                }
            };

            var res = await this.dynamoDB.GetItemAsync(getItemReq);
            if (res.Item.Count == 0) return null;

            var itemAsDoc = Document.FromAttributeMap(res.Item);

            return JsonSerializer.Deserialize<Customer>(itemAsDoc.ToJson());

        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            customer.UpdatedAt = DateTime.UtcNow;
            string customerAsJson = JsonSerializer.Serialize(customer);
            Dictionary<string, AttributeValue> customerAsItem = Document.FromJson(customerAsJson).ToAttributeMap();

            var createCutomerRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = customerAsItem
            };

            var res = await this.dynamoDB.PutItemAsync(createCutomerRequest);
            return res.HttpStatusCode == System.Net.HttpStatusCode.OK;

        }
    }
}
