using DesafioGPS.GpsService;
using DesafioGPS.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioGPS.Controllers;


[ApiController]
[Route("/Controller")]
public class PointOfInterestController:ControllerBase
{
    private GpsServices services;
    public PointOfInterestController(GpsServices gpsServices)
    {
        services = gpsServices;
    }

    [HttpPost("/Register/PointOfInterest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(PointOfInterest pointOfInterest)
    {
        var itfound = services.GetAsyncPoint(pointOfInterest.Id);
        if (itfound is null) { return NotFound(); }
        await services.CreateAsyncPoint(pointOfInterest);
        return Ok(pointOfInterest);
    }
    [HttpGet("/Get/PointOfInterest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PointOfInterest>> get(string id)
    {
        var pointOfInterest = await services.GetAsyncPoint(id);
        if (pointOfInterest is null)
        {
            return NotFound();
        }
        return pointOfInterest;
    }


    [HttpPut("/Put/PointOfInterest-{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(string id, PointOfInterest pointOfInterest)
    {
        var itfound = await services.GetAsyncPoint(id);

        if (itfound is null)
        {
            return NotFound();
        }

        pointOfInterest.Id = itfound.Id;

        await services.UpdateAsyncPoint(id, pointOfInterest);

        return NoContent();
    }

    [HttpDelete("/Delete/PointOfInterest-{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(string id)
    {
        var itfound = await services.GetAsyncPoint(id);

        if (itfound is null)
        {
            return NotFound();
        }

        await services.RemoveAsyncPoint(id);

        return NoContent();
    }
}
