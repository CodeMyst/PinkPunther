using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PinkPunther.Backend.Models;

namespace PinkPunther.Backend.Services;

public class MongoDbService
{
    public IMongoCollection<Pun> PunsCollection { get; }

    public IMongoCollection<PunSubmission> PunSubmissionsCollection { get; }

    public MongoDbService(IOptions<PinkPuntherDatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

        PunsCollection = mongoDb.GetCollection<Pun>(dbSettings.Value.PunsCollectionName);
        PunSubmissionsCollection =
            mongoDb.GetCollection<PunSubmission>(dbSettings.Value.PunSubmissionsCollectionName);
    }
}