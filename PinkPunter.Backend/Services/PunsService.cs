using MongoDB.Driver;
using PinkPunter.Backend.Models;

namespace PinkPunter.Backend.Services;

public class PunsService
{
    private readonly IMongoCollection<Pun> _punsCollection;

    public PunsService(MongoDbService mongoDbService)
    {
        _punsCollection = mongoDbService.PunsCollection;
    }

    public async Task<List<Pun>> GetAsync() => await _punsCollection.Find(_ => true).ToListAsync();
}