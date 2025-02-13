using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WebLab2_.NETAPI.Models;

namespace WebLab2_.NETAPI.Data
{
    public class PotatoCRUD
    {
        private IMongoDatabase _potato;

        public PotatoCRUD(string potatoDatabase)
        {
            var envconnection = System.Environment.GetEnvironmentVariable("LABB_2_CONNECTION");
            var client = new MongoClient(envconnection);
            _potato = client.GetDatabase(potatoDatabase);
        }


        //Create
        public async Task<List<Potatoes>> AddPotato(string table, Potatoes potato)
        {
            var Collection = _potato.GetCollection<Potatoes>(table);
            await Collection.InsertOneAsync(potato);
            return Collection.AsQueryable().ToList();
        }

        //Get all
        public async Task<List<Potatoes>> GetAllPotatoes(string table)
        {
            var Collection = _potato.GetCollection<Potatoes>(table);
            var potatoes = await Collection.AsQueryable().ToListAsync();
            return potatoes;
        }

        //Get potato by id
        public async Task<Potatoes> GetPotatoById(string table, string id)
        {
            var Collection = _potato.GetCollection<Potatoes>(table);
            var potato = await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
            return potato;
        }


        //Update potato
        public async Task<string> UpdatePotatoById(string table, string id, Potatoes potato)
        {
            var Collection = _potato.GetCollection<Potatoes>(table);
            var update = Builders<Potatoes>.Update
                .Set(x => x.Name, potato.Name)
                .Set(x => x.Type, potato.Type)
                .Set(x => x.Rank, potato.Rank);
            var updatedPotato = await Collection.UpdateOneAsync(x => x.Id == id, update);
            return "Potato updated";
        }


        //Delete potato
        public async Task<string> DeletePotatoById(string table, String id)
        {
            var Collection = _potato.GetCollection<Potatoes>(table);
            var potato = await Collection.DeleteOneAsync(x => x.Id == id);
            return "Potato deleted";
        }
    }
}
