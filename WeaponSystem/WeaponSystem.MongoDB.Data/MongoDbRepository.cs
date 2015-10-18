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

        private static readonly IMongoCollection<BsonDocument> Collection =
            Database.GetCollection<BsonDocument>("WeaponCategoies");

        public async Task<IList<WeaponCategory>> GetWeaponCategories()
        {
            var wc = (await Collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<WeaponCategory>(bs)).ToList();

            return wc;
        }
    }
}
