using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PinkPunter.Backend.Models;

namespace PinkPunter.Backend.Services;

public class MongoDbService
{
    public IMongoCollection<Pun> PunsCollection { get; }

    public IMongoCollection<PunSubmission> PunSubmissionsCollection { get; }

    public MongoDbService(IOptions<PinkPunterDatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

        PunsCollection = mongoDb.GetCollection<Pun>(dbSettings.Value.PunsCollectionName);
        PunSubmissionsCollection =
            mongoDb.GetCollection<PunSubmission>(dbSettings.Value.PunSubmissionsCollectionName);
    }
}