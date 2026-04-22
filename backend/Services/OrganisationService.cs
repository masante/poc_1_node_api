using MongoDB.Driver;
using PersonApi.Models;

namespace PersonApi.Services;

public interface IOrganisationService
{
    Task<List<Organisation>> GetAllOrganisationsAsync();
    Task<Organisation> GetOrganisationByIdAsync(string id);
    Task<Organisation> GetOrganisationByOrgIdAsync(string orgId);
    Task<Organisation> CreateOrganisationAsync(Organisation organisation);
    Task<bool> UpdateOrganisationAsync(string id, Organisation organisation);
    Task<bool> DeleteOrganisationAsync(string id);
    Task<List<Organisation>> GetOrganisationsByBusinessTypeAsync(string businessType);
}

public class OrganisationService : IOrganisationService
{
    private readonly IMongoCollection<Organisation> _organisationCollection;

    public OrganisationService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("epr");
        _organisationCollection = database.GetCollection<Organisation>("organisations");
    }

    public async Task<List<Organisation>> GetAllOrganisationsAsync()
    {
        return await _organisationCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Organisation> GetOrganisationByIdAsync(string id)
    {
        return await _organisationCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Organisation> GetOrganisationByOrgIdAsync(string orgId)
    {
        return await _organisationCollection.Find(o => o.OrgId == orgId).FirstOrDefaultAsync();
    }

    public async Task<Organisation> CreateOrganisationAsync(Organisation organisation)
    {
        await _organisationCollection.InsertOneAsync(organisation);
        return organisation;
    }

    public async Task<bool> UpdateOrganisationAsync(string id, Organisation organisation)
    {
        organisation.Id = id;
        var result = await _organisationCollection.ReplaceOneAsync(o => o.Id == id, organisation);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteOrganisationAsync(string id)
    {
        var result = await _organisationCollection.DeleteOneAsync(o => o.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<List<Organisation>> GetOrganisationsByBusinessTypeAsync(string businessType)
    {
        return await _organisationCollection.Find(o => o.BusinessType == businessType).ToListAsync();
    }
}
