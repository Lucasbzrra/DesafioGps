using DesafioGPS.GpsService;
using DesafioGPS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

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

    [HttpPost("/Register/StartingPoint")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(StartingPoint startingPoint)
    {
        var itfound = services.GetAsyncPoint(startingPoint.Id);
        if(itfound is null) { return NotFound(); }
        await services.CreateAsyncStarting(startingPoint);
        return Ok(startingPoint);
    }
    [HttpGet("/Get/StartingPoint")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StartingPoint>> get(string id)
    {
        var startingpoint=await services.GetAsyncStarting(id);
        if(startingpoint is null)
        {
            return NotFound();
        }
        return startingpoint;
    }
    [HttpPut("/Put/StartingPoint-{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(string id, StartingPoint startingPoint)
    {
        var itfound = await services.GetAsyncStarting(id);

        if (itfound is null)
        {
            return NotFound();
        }

        startingPoint.Id = itfound.Id;

        await services.UpdateAsyncStarting(id, startingPoint);

        return NoContent();
    }

    [HttpDelete("/Delete/StartingPoint-{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(string id)
    {
        var itfound = await services.GetAsyncStarting(id);

        if (itfound is null)
        {
            return NotFound();
        }

        await services.RemoveAsyncStarting(id);

        return NoContent();
    }

    [HttpGet("/Calculet-Distancia-{id}")]
    public async Task<Object> List(string id)
    {
        var itfound=services.GetAsyncStarting(id);

       return await services.GetListPOIs(id);
    }
}
