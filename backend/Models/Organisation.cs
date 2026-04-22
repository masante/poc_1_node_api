using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonApi.Models;

public class Organisation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("orgId")]
    public string OrgId { get; set; }

    [BsonElement("systemReference")]
    public ObjectId SystemReference { get; set; }

    [BsonElement("schemaVersion")]
    public int SchemaVersion { get; set; }

    [BsonElement("version")]
    public int Version { get; set; }

    [BsonElement("wasteProcessingTypes")]
    public List<string> WasteProcessingTypes { get; set; }

    [BsonElement("reprocessingNations")]
    public List<string> ReprocessingNations { get; set; }

    [BsonElement("businessType")]
    public string BusinessType { get; set; }

    [BsonElement("registrations")]
    public List<Registration> Registrations { get; set; }

    [BsonElement("accreditations")]
    public List<Accreditation> Accreditations { get; set; }

    [BsonElement("contactDetails")]
    public User ContactDetails { get; set; }

    [BsonElement("formSubmissionRawDataId")]
    public string FormSubmissionRawDataId { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("partnership")]
    public Partnership Partnership { get; set; }
}

public class Partnership
{
    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("partners")]
    public List<Partner> Partners { get; set; }
}

public class Partner
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("type")]
    public string Type { get; set; }
}
