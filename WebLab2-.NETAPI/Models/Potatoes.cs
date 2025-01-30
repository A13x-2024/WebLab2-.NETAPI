using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;



namespace WebLab2_.NETAPI.Models
{
    public class Potatoes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Name")] //Kakashi-Potato
        public string Name { get; set; }
        
        [BsonElement("Type")] //Legendary
        public string Type { get; set; }

        [BsonElement("Rank")] //9001 (over 9000)
        public int Rank { get; set; }

    }
}
