using MongoDB.Driver;
using PersonApi.Models;

namespace PersonApi.Services;

public interface IPersonService
{
    Task<List<Person>> GetAllPersonsAsync();
    Task<Person> GetPersonByIdAsync(string id);
    Task<Person> CreatePersonAsync(Person person);
    Task<bool> UpdatePersonAsync(string id, Person person);
    Task<bool> DeletePersonAsync(string id);
}

public class PersonService : IPersonService
{
    private readonly IMongoCollection<Person> _personCollection;

    public PersonService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("epr");
        _personCollection = database.GetCollection<Person>("persons");
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _personCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Person> GetPersonByIdAsync(string id)
    {
        return await _personCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        await _personCollection.InsertOneAsync(person);
        return person;
    }

    public async Task<bool> UpdatePersonAsync(string id, Person person)
    {
        person.Id = id;
        var result = await _personCollection.ReplaceOneAsync(p => p.Id == id, person);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeletePersonAsync(string id)
    {
        var result = await _personCollection.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }
}
