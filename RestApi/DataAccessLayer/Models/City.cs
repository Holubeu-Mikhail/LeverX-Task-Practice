using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    [BsonIgnoreExtraElements(true)]
    [Table("Cities")]
    public class City : BaseModel
    {
        public string Name { get; set; }

        public int Code { get; set; }
    }
}
