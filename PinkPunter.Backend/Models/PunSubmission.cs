using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PinkPunter.Backend.Models;

/// <summary>
/// Holds a submission for adding a pun to the website. Can be approved or rejected.
///
/// All pun submissions are stored in the DB, even after they have been approved or rejected.
/// </summary>
public class PunSubmission
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    /// <summary>
    /// Type of pun. Can be Question/Answer or Statement.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public PunType Type { get; set; }
    
    /// <summary>
    /// When the pun submission has been made.
    /// </summary>
    public DateTime SubmittedAt { get; set; }

    /// <summary>
    /// Pun question.
    /// </summary>
    public string Question { get; set; } = null!;
    
    /// <summary>
    /// Pun answer. If the type is <see cref="PunType.Statement"/> then this is null.
    /// </summary>
    public string? Answer { get; set; }
}