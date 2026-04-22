using MongoDB.Bson.Serialization.Attributes;

namespace PersonApi.Models;

public class User
{
    [BsonElement("fullName")]
    public string FullName { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("role")]
    public string Role { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }
}

public class Address
{
    [BsonElement("line1")]
    public string Line1 { get; set; }

    [BsonElement("line2")]
    public string Line2 { get; set; }

    [BsonElement("town")]
    public string Town { get; set; }

    [BsonElement("county")]
    public string County { get; set; }

    [BsonElement("country")]
    public string Country { get; set; }

    [BsonElement("postcode")]
    public string Postcode { get; set; }
}
