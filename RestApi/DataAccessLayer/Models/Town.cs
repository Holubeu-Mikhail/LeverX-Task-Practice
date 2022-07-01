using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    public class Town : BaseModel
    {
        public string Name { get; set; }

        public int Code { get; set; }
    }
}
