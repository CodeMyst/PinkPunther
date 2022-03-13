using MongoDB.Driver;
using PinkPunter.Backend.Models;

namespace PinkPunter.Backend.Services;

public class PunSubmissionsService
{
    private readonly IMongoCollection<PunSubmission> _collection;

    public PunSubmissionsService(MongoDbService mongoDbService)
    {
        _collection = mongoDbService.PunSubmissionsCollection;
    }
    
    public async Task CreateSubmissionAsync(PunSubmission submission) => await _collection.InsertOneAsync(submission);
}