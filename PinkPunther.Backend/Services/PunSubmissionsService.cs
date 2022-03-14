using MongoDB.Driver;
using PinkPunther.Backend.Models;

namespace PinkPunther.Backend.Services;

public class PunSubmissionsService
{
    private readonly IMongoCollection<PunSubmission> _collection;

    public PunSubmissionsService(MongoDbService mongoDbService)
    {
        _collection = mongoDbService.PunSubmissionsCollection;
    }

    public async Task CreateSubmissionAsync(PunSubmission submission) => await _collection.InsertOneAsync(submission);

    public async Task<PagingCollection<PunSubmission>> GetSubmissionsAsync(int page, int pageSize)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Page has to be greater than 0.");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException("Page size has to be greater than 0.");
        }

        var query = _collection.Find(_ => true);

        var total = await query.CountDocumentsAsync();

        var items = await query.SortBy(submission => submission.SubmittedAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return new PagingCollection<PunSubmission>
        {
            Page = page,
            PageSize = pageSize,
            TotalPages = (total + pageSize - 1) / pageSize,
            Items = items
        };
    }
}