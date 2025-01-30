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
        
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Rank")]
        public int Rank { get; set; }

    }
}
