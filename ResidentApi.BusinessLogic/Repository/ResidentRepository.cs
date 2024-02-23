using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResidentApi.BusinessLogic.Models;

namespace ResidentApi.BusinessLogic.Repository
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly IMongoCollection<ResidentModel> _residentModelCollection;

        public ResidentRepository(IOptions<MongoDbSettings> config)
        {
            var mongoClient = new MongoClient(config.Value.ConnectionString);
            var mongoDbName = mongoClient.GetDatabase(config.Value.DatabaseName);

            _residentModelCollection = mongoDbName.GetCollection<ResidentModel>(config.Value.CollectionName);
        }

        public async Task<ResidentModel> GetResidentAsync(string id)
        {
            return await _residentModelCollection.Find(x => x.ResidentId == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<long> GetTotalDocumentsCountAsync()
        {
            // Get total count of all documents in the collection
            long totalDocuments = await _residentModelCollection.CountDocumentsAsync(FilterDefinition<ResidentModel>.Empty);
            return totalDocuments;
        }

        public async Task SaveResidentAsync(ResidentModel model)
        {
            var residentModel = await GetResidentAsync(model.ResidentId);

            if(residentModel == null)
            {
                await _residentModelCollection.InsertOneAsync(model).ConfigureAwait(false);
            }
            else
            {
                var filter = Builders<ResidentModel>.Filter.Eq("residentId", residentModel.ResidentId);
                var update = Builders<ResidentModel>.Update
                    .Set("firstName", model.FirstName)
                    .Set("lastName", model.LastName)
                    .Set("apartmentNumber", model.ApartmentNumber)
                    .Set("totalBalance", model.TotalBalance)
                    .Set("email", model.Email);
                await _residentModelCollection.UpdateOneAsync(filter, update);
            }
        }
    }
}
