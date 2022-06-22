using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    public class BaseModel
    {
        [BsonId]
        public virtual int Id { get; set; }
    }
}
