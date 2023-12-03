using MongoDB.Bson.Serialization.Attributes;

namespace DesafioGPS.Models;

public class StartingPoint
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ? Id { get; set; }

    [BsonElement("StartingPoint")]
    public string Name { get; set; } = null!;
    public int X { get; set; }
    public int Y { get; set; }
}
