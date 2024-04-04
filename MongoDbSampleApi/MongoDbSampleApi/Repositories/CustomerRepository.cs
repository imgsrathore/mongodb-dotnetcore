using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbSampleApi.Entities;
using MongoDbSampleApi.Interfaces;

namespace MongoDbSampleApi.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly IMongoCollection<Customer> _customer;
        public CustomerRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("IronStoreDb");
            var collection = mongoDatabase.GetCollection<Customer>(nameof(Customer));
            _customer = collection;
        }
        public async Task<bool> IsCustomerExistsAsync(string EmailId)
        {
            var filter = Builders<Customer>.Filter.Eq(xx => xx.EmailId, EmailId);
            var result = await _customer.Find(filter).FirstOrDefaultAsync();
            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ObjectId> CreateCustomerAsync(Customer customer)
        {
            await _customer.InsertOneAsync(customer);
            return customer.Id;
        }

        public async Task<bool> LoginAsync(CustomerCredential credential)
        {
            var filterBulder = Builders<Customer>.Filter;
            var multiFilter = filterBulder.Eq(xx => xx.EmailId.ToLower(), credential.EmailId) & filterBulder.Eq(xx => xx.Password, credential.Password);

                
            var result = await _customer.Find(multiFilter).FirstOrDefaultAsync();
            
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
