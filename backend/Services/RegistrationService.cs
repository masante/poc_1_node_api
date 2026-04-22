using MongoDB.Driver;
using PersonApi.Models;

namespace PersonApi.Services;

public interface IRegistrationService
{
    Task<List<Registration>> GetAllRegistrationsAsync();
    Task<Registration> GetRegistrationByIdAsync(string id);
    Task<List<Registration>> GetRegistrationsByMaterialAsync(string material);
    Task<List<Registration>> GetRegistrationsByWasteProcessingTypeAsync(string wasteProcessingType);
    Task<List<Registration>> GetRegistrationsByStatusAsync(string status);
    Task<Registration> CreateRegistrationAsync(Registration registration);
    Task<bool> UpdateRegistrationAsync(string id, Registration registration);
    Task<bool> DeleteRegistrationAsync(string id);
    Task<List<Registration>> GetRegistrationsByOrganisationAsync(string orgId);
}

public class RegistrationService : IRegistrationService
{
    private readonly IMongoCollection<Registration> _registrationCollection;

    public RegistrationService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("epr");
        _registrationCollection = database.GetCollection<Registration>("registrations");
    }

    public async Task<List<Registration>> GetAllRegistrationsAsync()
    {
        return await _registrationCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Registration> GetRegistrationByIdAsync(string id)
    {
        return await _registrationCollection.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Registration>> GetRegistrationsByMaterialAsync(string material)
    {
        return await _registrationCollection.Find(r => r.Material == material).ToListAsync();
    }

    public async Task<List<Registration>> GetRegistrationsByWasteProcessingTypeAsync(string wasteProcessingType)
    {
        return await _registrationCollection.Find(r => r.WasteProcessingType == wasteProcessingType).ToListAsync();
    }

    public async Task<List<Registration>> GetRegistrationsByStatusAsync(string status)
    {
        return await _registrationCollection.Find(r => r.Status == status).ToListAsync();
    }

    public async Task<Registration> CreateRegistrationAsync(Registration registration)
    {
        await _registrationCollection.InsertOneAsync(registration);
        return registration;
    }

    public async Task<bool> UpdateRegistrationAsync(string id, Registration registration)
    {
        registration.Id = id;
        var result = await _registrationCollection.ReplaceOneAsync(r => r.Id == id, registration);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteRegistrationAsync(string id)
    {
        var result = await _registrationCollection.DeleteOneAsync(r => r.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<List<Registration>> GetRegistrationsByOrganisationAsync(string orgId)
    {
        // Note: This would require additional context or joining with Organisation collection
        // For now, returning empty list as registrations are nested in Organisation
        return await Task.FromResult(new List<Registration>());
    }
}
