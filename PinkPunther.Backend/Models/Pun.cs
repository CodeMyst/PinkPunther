using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PinkPunther.Backend.Models;

/// <summary>
/// Pun object, holds one pun.
/// </summary>
public class Pun
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
    /// When the pun was was approved by admins and added to the list.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// List of all ratings the pun has gotten.
    /// </summary>
    public List<Rating>? Ratings { get; set; }

    /// <summary>
    /// Pun question.
    /// </summary>
    public string Question { get; set; } = null!;
    
    /// <summary>
    /// Pun answer. If the type is <see cref="PunType.Statement"/> then this is null.
    /// </summary>
    public string? Answer { get; set; }
}