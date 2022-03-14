using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PinkPunther.Backend.Models;

/// <summary>
/// A single rating on a pun.
/// </summary>
public class Rating
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    /// <summary>
    /// Rating value, from 1-5.
    /// </summary>
    [Range(1, 5)]
    public uint Stars { get; set; }
    
    /// <summary>
    /// When the rating has been made.
    /// </summary>
    public DateTime RatedAt { get; set; }
}