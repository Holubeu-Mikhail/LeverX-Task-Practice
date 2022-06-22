using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    public class Product : BaseModel
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int TypeId { get; set; }
    }
}
