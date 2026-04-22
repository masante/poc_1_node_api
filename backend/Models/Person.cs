using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonApi.Models;

public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("firstname")]
    public string FirstName { get; set; }

    [BsonElement("lastname")]
    public string LastName { get; set; }
}
