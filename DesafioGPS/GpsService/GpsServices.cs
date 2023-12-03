using DesafioGPS.Data;
using DesafioGPS.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DesafioGPS.GpsService;

public class GpsServices
{
    private readonly IMongoCollection<StartingPoint> _startingPoint;
    private readonly IMongoCollection<PointOfInterest> _pointOfInterest;

    public GpsServices(
        IOptions<GpsDatabaseSettings> opts)
    {
        var mongoClient = new MongoClient(
            opts.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            opts.Value.DatabaseName);

        _startingPoint = mongoDatabase.GetCollection<StartingPoint>(
            opts.Value.StartingPointCollection);


        _pointOfInterest = mongoDatabase.GetCollection<PointOfInterest>(
           opts.Value.PointOfInterestCollection);
    }

    public async Task<List<StartingPoint>> GetAsyncStarting() =>
        await _startingPoint.Find(_ => true).ToListAsync();

    public async Task<StartingPoint?> GetAsyncStarting(string id) =>
        await _startingPoint.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsyncStarting(StartingPoint newBook) =>
        await _startingPoint.InsertOneAsync(newBook);

    public async Task UpdateAsyncStarting(string id, StartingPoint updatedBook) =>
        await _startingPoint.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsyncStarting(string id) =>
        await _startingPoint.DeleteOneAsync(x => x.Id == id);


    //
    public async Task<List<PointOfInterest>> GetAsyncPoint() =>
        await _pointOfInterest.Find(_ => true).ToListAsync();

    public async Task<PointOfInterest?> GetAsyncPoint(string id) =>
        await _pointOfInterest.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsyncPoint(PointOfInterest newBook) =>
        await _pointOfInterest.InsertOneAsync(newBook);

    public async Task UpdateAsyncPoint(string id, PointOfInterest updatedBook) =>
        await _pointOfInterest.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsyncPoint(string id) =>
        await _pointOfInterest.DeleteOneAsync(x => x.Id == id);

    public List<Object> GetListPOIs()
    {
        var list = new List<Object>();
        var lista = _pointOfInterest.Find(x => true).ToList();
        foreach (var item in lista)
        {
            var teste = CalculateDistance.IsNext(item, startingPoint);
            if (teste)
            {
                lista.Add(item);
            }
        }
    }
}

