using DesafioGPS.Models;

namespace DesafioGPS.GpsService;

public class CalculateDistance
{
    public static bool IsNext(PointOfInterest pointOfInterest, StartingPoint startingPoint)
    {
        bool between(int value)=> value<=10 ? true : false;
        int value = Calculo(pointOfInterest.X, pointOfInterest.Y, startingPoint.X, startingPoint.Y);
        return between(value);
    }
    private static int Calculo(int x, int y, int x2, int y2)
    {
        int result = Convert.ToInt32(Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2)));
        return result;
    }
}
