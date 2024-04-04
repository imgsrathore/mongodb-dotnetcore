using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbSampleApi.Entities;
using MongoDbSampleApi.Interfaces;

namespace MongoDbSampleApi.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly IMongoCollection<Product> _products;
        public ProductRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("IronStoreDb");
            var collection = mongoDatabase.GetCollection<Product>(nameof(Product));
            _products = collection;
        }
        public async Task<ObjectId> CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
            return product.Id;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _products.Find(_ => true).ToListAsync();
            return products;
        }

        public async Task<bool> UpdateProductAsync(ObjectId Id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(xx => xx.Id, Id);
            var update = Builders<Product>.Update
                    .Set(x => x.ProductPrice, product.ProductPrice)
                    .Set(x => x.ProductDescription, product.ProductDescription);
            var result = await _products.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public async Task<bool> DeleteProductAsync(ObjectId Id)
        {
            var filter = Builders<Product>.Filter.Eq(xx => xx.Id, Id);
            var delete = await _products.DeleteOneAsync(filter);
            return delete.DeletedCount == 1;
        }
    }
}
