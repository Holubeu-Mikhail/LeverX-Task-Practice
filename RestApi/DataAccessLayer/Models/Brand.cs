using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    public class Brand : BaseModel
    {
        public string Name { get; set; }

        public Guid CityId { get; set; }

        public string Description { get; set; }
    }
}
