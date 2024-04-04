using MongoDB.Bson;
using MongoDbSampleApi.Entities;

namespace MongoDbSampleApi.Interfaces
{
    public interface IProduct
    {
        Task<ObjectId> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> UpdateProductAsync(ObjectId Id, Product product);
        Task<bool> DeleteProductAsync(ObjectId Id);
    }
}
