using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    public class ProductType : BaseModel
    {
        public string Name { get; set; }
    }
}
