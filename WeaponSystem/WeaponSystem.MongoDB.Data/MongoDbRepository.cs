namespace WeaponSystem.MongoDb.Data
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using WeaponSystem.Models;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;
    using MongoDB.Driver.Core.Clusters;

    public class MongoDbRepository
    {
        private const string ConnectionString = "mongodb://ninja:saltninja@ds041164.mongolab.com:41164/weapon-system";
        private static readonly MongoClient Client = new MongoClient(ConnectionString);
        private static readonly IMongoDatabase Database = Client.GetDatabase("weapon-system");

        private static readonly IMongoCollection<BsonDocument> WeaponCategoriesCollection = Database.GetCollection<BsonDocument>("WeaponCategoies");
        private static readonly IMongoCollection<BsonDocument> TargetsCategoriesCollection = Database.GetCollection<BsonDocument>("TargetCategories");
        private static readonly IMongoCollection<BsonDocument> CountriesCollection = Database.GetCollection<BsonDocument>("Countries");
        private static readonly IMongoCollection<BsonDocument> ManufacturersCollection = Database.GetCollection<BsonDocument>("Manufacturers");

        public async Task<IList<WeaponCategory>> GetWeaponCategories()
        {
            var weaponCategories = (await WeaponCategoriesCollection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<WeaponCategory>(bs)).ToList();

            return weaponCategories;
        }

        public async Task<IList<TargetCategory>> GetTargetCategories()
        {
            var targetCategories = (await TargetsCategoriesCollection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<TargetCategory>(bs)).ToList();

            return targetCategories;
        }


        public async Task<IList<Country>> GetCountries()
        {
            var countries = (await CountriesCollection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Country>(bs)).ToList();

            return countries;
        }

        public async Task<IList<Manufacturer>> GetManufacturers()
        {
            var manufacturers = (await ManufacturersCollection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Manufacturer>(bs)).ToList();

            return manufacturers;
        }
    }
}
