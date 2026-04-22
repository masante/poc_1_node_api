using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonApi.Models;

public class Accreditation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("formSubmissionTime")]
    public DateTime FormSubmissionTime { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }

    [BsonElement("material")]
    public string Material { get; set; }

    [BsonElement("wasteProcessingType")]
    public string WasteProcessingType { get; set; }

    [BsonElement("prnIssuance")]
    public PrnIssuance PrnIssuance { get; set; }

    [BsonElement("businessPlan")]
    public List<BusinessPlanItem> BusinessPlan { get; set; }

    [BsonElement("contactDetails")]
    public User ContactDetails { get; set; }

    [BsonElement("submitterContactDetails")]
    public User SubmitterContactDetails { get; set; }

    [BsonElement("samplingInspectionPlan")]
    public List<string> SamplingInspectionPlan { get; set; }

    [BsonElement("formSubmissionRawDataId")]
    public string FormSubmissionRawDataId { get; set; }

    [BsonElement("siteAddress")]
    public Address SiteAddress { get; set; }

    [BsonElement("recyclingProcess")]
    public List<string> RecyclingProcess { get; set; }

    [BsonElement("overseasSites")]
    public List<string> OverseasSites { get; set; }
}

public class PrnIssuance
{
    [BsonElement("plannedIssuance")]
    public string PlannedIssuance { get; set; }

    [BsonElement("signatories")]
    public List<User> Signatories { get; set; }

    [BsonElement("prnIncomeBusinessPlan")]
    public List<PrnIncomeItem> PrnIncomeBusinessPlan { get; set; }
}

public class PrnIncomeItem
{
    [BsonElement("percentIncomeSpent")]
    public int PercentIncomeSpent { get; set; }

    [BsonElement("usageDescription")]
    public string UsageDescription { get; set; }

    [BsonElement("detailedExplanation")]
    public string DetailedExplanation { get; set; }
}

public class BusinessPlanItem
{
    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("detailedDescription")]
    public string DetailedDescription { get; set; }

    [BsonElement("percentSpent")]
    public int PercentSpent { get; set; }
}
