using MongoDB.Driver;
using PersonApi.Models;

namespace PersonApi.Services;

public interface IAccreditationService
{
    Task<List<Accreditation>> GetAllAccreditationsAsync();
    Task<Accreditation> GetAccreditationByIdAsync(string id);
    Task<List<Accreditation>> GetAccreditationsByMaterialAsync(string material);
    Task<List<Accreditation>> GetAccreditationsByWasteProcessingTypeAsync(string wasteProcessingType);
    Task<List<Accreditation>> GetAccreditationsByStatusAsync(string status);
    Task<Accreditation> CreateAccreditationAsync(Accreditation accreditation);
    Task<bool> UpdateAccreditationAsync(string id, Accreditation accreditation);
    Task<bool> DeleteAccreditationAsync(string id);
}

public class AccreditationService : IAccreditationService
{
    private readonly IMongoCollection<Accreditation> _accreditationCollection;

    public AccreditationService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("epr");
        _accreditationCollection = database.GetCollection<Accreditation>("accreditations");
    }

    public async Task<List<Accreditation>> GetAllAccreditationsAsync()
    {
        return await _accreditationCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Accreditation> GetAccreditationByIdAsync(string id)
    {
        return await _accreditationCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Accreditation>> GetAccreditationsByMaterialAsync(string material)
    {
        return await _accreditationCollection.Find(a => a.Material == material).ToListAsync();
    }

    public async Task<List<Accreditation>> GetAccreditationsByWasteProcessingTypeAsync(string wasteProcessingType)
    {
        return await _accreditationCollection.Find(a => a.WasteProcessingType == wasteProcessingType).ToListAsync();
    }

    public async Task<List<Accreditation>> GetAccreditationsByStatusAsync(string status)
    {
        return await _accreditationCollection.Find(a => a.Status == status).ToListAsync();
    }

    public async Task<Accreditation> CreateAccreditationAsync(Accreditation accreditation)
    {
        await _accreditationCollection.InsertOneAsync(accreditation);
        return accreditation;
    }

    public async Task<bool> UpdateAccreditationAsync(string id, Accreditation accreditation)
    {
        accreditation.Id = id;
        var result = await _accreditationCollection.ReplaceOneAsync(a => a.Id == id, accreditation);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAccreditationAsync(string id)
    {
        var result = await _accreditationCollection.DeleteOneAsync(a => a.Id == id);
        return result.DeletedCount > 0;
    }
}
