using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DesafioGPS.Models;

public class PointOfInterest
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ? Id { get; set; }

    [BsonElement("PointOfInterest")]
    public string Name { get; set; } = null!;
    [Required]
    [Range(0,10000000)]
    public int X { get; set; }
    [Required]
    [Range(0, 10000000)]
    public int Y { get; set; }
}
