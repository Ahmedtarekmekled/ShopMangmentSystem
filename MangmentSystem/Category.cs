using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MangmentSystem
{
    internal class Category
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("categoryname")]
        public string categoryname { get; set; }

        public Category(string categoryname)
        {
            this.categoryname = categoryname;
        }
    }
}
