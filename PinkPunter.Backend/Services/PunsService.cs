using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PinkPunter.Backend.Models;

namespace PinkPunter.Backend.Services;

public class PunsService
{
    private readonly IMongoCollection<Pun> _punsCollection;

    public PunsService(IOptions<PinkPunterDatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

        _punsCollection = mongoDb.GetCollection<Pun>(dbSettings.Value.PunsCollectionName);
    }

    public async Task<List<Pun>> GetAsync() => await _punsCollection.Find(_ => true).ToListAsync();
}