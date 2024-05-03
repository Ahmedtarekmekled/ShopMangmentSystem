using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MangmentSystem
{
    public class Item
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("stock")]
        public int Stock { get; set; }

        [BsonElement("category")]
        public ObjectId CategoryId { get; set; } // Store ObjectId of associated Category

        public Item(string name, decimal price, int stock, ObjectId categoryId)
        {
            Name = name;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }
    }
}

