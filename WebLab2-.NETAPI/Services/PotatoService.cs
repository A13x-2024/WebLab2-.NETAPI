using MongoDB.Driver;
using WebLab2_.NETAPI.Models;

namespace WebLab2_.NETAPI.Services
{
    public class PotatoServices
    {
        private readonly IMongoCollection<Potatis> _potatis;

        public PotatoServices(PotatoLogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _potatis = database.GetCollection<Potatis>(settings.PotatisCollectionName);
        }


        // CRUD operations
        //Get
        public async Task<List<Potatis>> GetAsync() =>
            await _potatis.Find(potatis => true).ToListAsync();

        //Get by id
        public async Task<Potatis> GetByIdAsync(int id) =>
            await _potatis.Find<Potatis>(potatis => potatis.Id == id).FirstOrDefaultAsync();

        //Create
        public async Task<Potatis> CreateAsync(Potatis newPotatis) =>
            await _potatis.InsertOneAsync(newPotatis).ContinueWith(t => newPotatis);

        //Update
        public async Task UpdateAsync(int id, Potatis updatedPotatis) =>
            await _potatis.ReplaceOneAsync(potatis => potatis.Id == id, updatedPotatis);

        //Delete
        public async Task RemoveAsync(int id) =>
            await _potatis.DeleteOneAsync(potatis => potatis.Id == potatis.Id);
    }
}
