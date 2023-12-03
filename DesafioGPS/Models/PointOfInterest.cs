using MongoDB.Bson.Serialization.Attributes;

namespace DesafioGPS.Models;

public class PointOfInterest
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("PointOfInterest")]
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
