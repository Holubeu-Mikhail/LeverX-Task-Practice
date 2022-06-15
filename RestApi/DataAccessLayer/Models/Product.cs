namespace DataAccessLayer.Models
{
    public class Product : BaseModel
    {
        public override int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int TypeId { get; set; }
    }
}
