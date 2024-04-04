using MongoDB.Bson;
using MongoDbSampleApi.Entities;

namespace MongoDbSampleApi.Interfaces
{
    public interface ICustomer
    {
        Task<bool> IsCustomerExistsAsync(string EmailId);
        Task<ObjectId> CreateCustomerAsync(Customer customer);
        Task<bool> LoginAsync(CustomerCredential credential);

    }
}
