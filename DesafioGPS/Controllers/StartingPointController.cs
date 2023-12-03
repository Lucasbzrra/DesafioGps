using DesafioGPS.GpsService;
using DesafioGPS.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioGPS.Controllers;

[ApiController]
[Route("/Controller")]
public class StartingPointController:ControllerBase
{
    private GpsServices services;
    public StartingPointController(GpsServices gpsServices)
    {
        services = gpsServices;
    }
    [HttpPost("/Register")]
    public async Task<IActionResult> Post(StartingPoint startingPoint)
    {
        await services.CreateAsyncStarting(startingPoint);

        return Ok(startingPoint);
    }
}
