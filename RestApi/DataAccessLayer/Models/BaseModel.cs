using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    public class BaseModel
    {
        [BsonId]
        public virtual Guid Id { get; set; }
    }
}
