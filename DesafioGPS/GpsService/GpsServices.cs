using DesafioGPS.Data;
using DesafioGPS.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
    //Método de para acessar o bando de dados e retornar os dados na collection StartingPoint
    public async Task<List<StartingPoint>> GetAsyncSListtarting() =>
        await _startingPoint.Find(_ => true).ToListAsync();

    public async Task<StartingPoint?> GetAsyncStarting(string id) =>
        await _startingPoint.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsyncStarting(StartingPoint startingPoint) =>
        await _startingPoint.InsertOneAsync(startingPoint);

    public async Task UpdateAsyncStarting(string id, StartingPoint updateStarting) =>
        await _startingPoint.ReplaceOneAsync(x => x.Id == id, updateStarting);

    public async Task RemoveAsyncStarting(string id) =>
        await _startingPoint.DeleteOneAsync(x => x.Id == id);


    //Método de para acessar o bando de dados e retornar os dados na collection PointOfInterest
    public async Task<List<PointOfInterest>> GetAsyncPointList() =>
        await _pointOfInterest.Find(_ => true).ToListAsync();

    public async Task<PointOfInterest?> GetAsyncPoint(string id) =>
        await _pointOfInterest.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsyncPoint(PointOfInterest pointOfInterest) =>
        await _pointOfInterest.InsertOneAsync(pointOfInterest);

    public async Task UpdateAsyncPoint(string id, PointOfInterest updatePoint) =>
        await _pointOfInterest.ReplaceOneAsync(x => x.Id == id, updatePoint);

    public async Task RemoveAsyncPoint(string id) =>
        await _pointOfInterest.DeleteOneAsync(x => x.Id == id);

    // Método para retornar a lista de Ponto de intereses proximos
    public async Task<List<Object>> GetListPOIs(string id)
    {
        var list = new List<Object>();
        var startingPoint = _startingPoint.Find(x => x.Id.Equals(id)).FirstOrDefault();
        var lista = _pointOfInterest.Find(x => true).ToList();

        foreach (var item in lista)
        {
            var teste = CalculateDistance.IsNext(item, startingPoint);
            if (teste)
            {
                list.Add(item);
            }
        }
        return list;
    }
}

