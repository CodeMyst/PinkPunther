using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PinkPunter.Backend.Models;

/// <summary>
/// Information used for submitting puns.
/// </summary>
public class PunSubmissionCreateInfo
{
    /// <summary>
    /// Type of pun. Can be Question/Answer or Statement.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public PunType Type { get; set; }

    /// <summary>
    /// Pun question.
    /// </summary>
    public string Question { get; set; } = null!;
    
    /// <summary>
    /// Pun answer. If the type is <see cref="PunType.Statement"/> then this is null.
    /// </summary>
    public string? Answer { get; set; }
}