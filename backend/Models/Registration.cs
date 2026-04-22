using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonApi.Models;

public class Registration
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

    [BsonElement("gridReference")]
    public string GridReference { get; set; }

    [BsonElement("wasteRegistrationNumber")]
    public string WasteRegistrationNumber { get; set; }

    [BsonElement("wasteManagementPermits")]
    public List<WasteManagementPermit> WasteManagementPermits { get; set; }

    [BsonElement("approvedPersons")]
    public List<User> ApprovedPersons { get; set; }

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

    [BsonElement("yearlyMetrics")]
    public YearlyMetrics YearlyMetrics { get; set; }

    [BsonElement("plantEquipmentDetails")]
    public string PlantEquipmentDetails { get; set; }

    [BsonElement("suppliers")]
    public string Suppliers { get; set; }

    [BsonElement("exportPorts")]
    public List<string> ExportPorts { get; set; }

    [BsonElement("overseasSites")]
    public List<string> OverseasSites { get; set; }

    [BsonElement("noticeAddress")]
    public Address NoticeAddress { get; set; }

    [BsonElement("accreditationId")]
    public string AccreditationId { get; set; }
}

public class WasteManagementPermit
{
    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("permitNumber")]
    public string PermitNumber { get; set; }

    [BsonElement("authorisedWeight")]
    public string AuthorisedWeight { get; set; }

    [BsonElement("permitWindow")]
    public string PermitWindow { get; set; }

    [BsonElement("exemptions")]
    public List<Exemption> Exemptions { get; set; }
}

public class Exemption
{
    [BsonElement("reference")]
    public string Reference { get; set; }

    [BsonElement("exemptionCode")]
    public string ExemptionCode { get; set; }
}

public class YearlyMetrics
{
    [BsonElement("year")]
    public string Year { get; set; }

    [BsonElement("input")]
    public WasteInput Input { get; set; }

    [BsonElement("rawMaterialInputs")]
    public List<MaterialInput> RawMaterialInputs { get; set; }

    [BsonElement("output")]
    public WasteOutput Output { get; set; }

    [BsonElement("metric")]
    public string Metric { get; set; }

    [BsonElement("productsMadeFromRecycling")]
    public List<RecycledProduct> ProductsMadeFromRecycling { get; set; }
}

public class WasteInput
{
    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("ukPackagingWaste")]
    public int UkPackagingWaste { get; set; }

    [BsonElement("nonUkPackagingWaste")]
    public int NonUkPackagingWaste { get; set; }

    [BsonElement("nonPackagingWaste")]
    public int NonPackagingWaste { get; set; }
}

public class MaterialInput
{
    [BsonElement("material")]
    public string Material { get; set; }

    [BsonElement("tonnage")]
    public int Tonnage { get; set; }
}

public class WasteOutput
{
    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("sentToAnotherSite")]
    public int SentToAnotherSite { get; set; }

    [BsonElement("contaminants")]
    public int Contaminants { get; set; }

    [BsonElement("processLoss")]
    public int ProcessLoss { get; set; }
}

public class RecycledProduct
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("weight")]
    public int Weight { get; set; }
}
