using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using System.Linq;
using RestaurantData;
using RestaurantData.DTOs;

namespace RestaurantSearcher
{
    public static class SyncRestaurants
    {
        private static readonly string TULSA = "51697";
        private static readonly string JENKS = "51434";

        [FunctionName("SyncRestaurants")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("RAPID-API-WORLD-WIDE-RESTAURANTS-TOKEN"));
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "worldwide-restaurants.p.rapidapi.com");

            var restaurants = new List<Datum>();
            restaurants.AddRange(await GetRestaurants(client, TULSA));
            var jenksRestaurants = await GetRestaurants(client, JENKS);
            restaurants.AddRange(jenksRestaurants.Where(jr => !restaurants.Any(tr => tr.location_id == jr.location_id)));
            SyncMongo(restaurants);

            return new OkObjectResult("OK");
        }

        private static async void SyncMongo(List<Datum> apiRestaurants)
        {
            // Bonus points: if restaurant exists in mongo but not the api, delete from mongo (restaurant closed)
            MongoClient dbClient = new MongoClient(Environment.GetEnvironmentVariable("MONGODB-DISCORDBOT-DB-CONNECTION-STRING"));

            var restaurantCollection = dbClient.GetDatabase("Restaurants")
                                               .GetCollection<BsonDocument>("Restaurants");

            var restaurantDocuments = await restaurantCollection.Find(new BsonDocument())
                                                                .ToListAsync();

            var mongoRestaurants = new List<MongoDatum>();
            var untrackedRestaurants = new List<BsonDocument>();

            foreach (var restaurantDocument in restaurantDocuments)
            {
                mongoRestaurants.Add(BsonSerializer.Deserialize<MongoDatum>(restaurantDocument));
            }

            foreach(var apiRestaurant in apiRestaurants)
            {
                if (!mongoRestaurants.Any(mongoRestaurant => mongoRestaurant.location_id == apiRestaurant.location_id))
                {
                    untrackedRestaurants.Add(apiRestaurant.ToBsonDocument());
                }
            }

            if (untrackedRestaurants.Count > 0) 
                restaurantCollection.InsertMany(untrackedRestaurants);
        }

        public static async Task<List<Datum>> GetRestaurants(HttpClient client, string location)
        {
            var restaurants = new List<Datum>();

            var restaurantData = await CallAPI(client, location, "50", "us-EN", "USD", "0");

            restaurants.AddRange(restaurantData.results.data);
            var remainingResults = Int32.Parse(restaurantData.results.paging.total_results) - 50;
            int callsNeeded = remainingResults / 50 + 1;
            int offset = 50;

            for(int i = 0; i < callsNeeded; i++)
            {
                restaurantData = await CallAPI(client, location, "50", "us-EN", "USD", offset.ToString());
                restaurants.AddRange(restaurantData.results.data);
                offset += 50;
            }

            return restaurants;
        }

        public static async Task<Root> CallAPI(
            HttpClient client, 
            string locationId, 
            string limit, 
            string language, 
            string currency, 
            string offset)
        {
            using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    location_id = locationId,
                    limit = limit,
                    language = language,
                    currency = currency,
                    offset = offset
                }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("https://worldwide-restaurants.p.rapidapi.com/search", jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Root>(responseString);
        }
    }
}
