using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using RestaurantData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantData.Services
{
    public class FinderService
    {
        private readonly IMemoryCache _memoryCache;
        public FinderService(IMemoryCache cache)
        {
            _memoryCache = cache;
        }

        public async Task<List<MongoDatum>> GetMasterList()
        {
            var cacheKey = "RestaurantMasterList";
            var masterListFromCache = (List<MongoDatum>)_memoryCache.Get(cacheKey);
            if (masterListFromCache == null)
            {
                var masterListFromDatabase = await RetrieveRestaurants();
                _memoryCache.Set(cacheKey, masterListFromDatabase);
                return masterListFromDatabase;
            }
            else
            {
                return masterListFromCache;
            }
        }

        private async static Task<List<MongoDatum>> RetrieveRestaurants()
        {
            MongoClient dbClient = new (Environment.GetEnvironmentVariable("MONGODB_DISCORDBOT_DB_CONNECTION_STRING"));
            var restaurantDocuments = await dbClient.GetDatabase("Restaurants")
                                                    .GetCollection<BsonDocument>("Restaurants")
                                                    .Find(new BsonDocument())
                                                    .ToListAsync();

            var restaurantList = new List<MongoDatum>();
            foreach (var restaurantDocument in restaurantDocuments)
            {
                restaurantList.Add(BsonSerializer.Deserialize<MongoDatum>(restaurantDocument));
            }

            return restaurantList;
        }
    }
}
