using Production_Incident_Assistant.Core.Entities;
using Production_Incident_Assistant.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Production_Incident_Assistant.Core.DTOs;

namespace Production_Incident_Assistant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IncidentService _incidentService;

    public IncidentsController(IncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    [HttpGet]
    public async Task<IEnumerable<IncidentResponseDto>> GetAllIncidentsAsync()
    {
        var incidents = await _incidentService.GetAllIncidentsAsync();

        return incidents.Select(incident => new IncidentResponseDto
        {
            Id = incident.Id,
            Title = incident.Title,
            Description = incident.Description,
            Severity = incident.Severity.ToString(),
            Status = incident.Status.ToString(),
            CreatedAt = incident.CreatedAt,
            ResolvedAt = incident.ResolvedAt
        });
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<IncidentDashboardDto>> GetDashboard()
    {
        var dashboard = await _incidentService.GetDashboardAsync();

        return Ok(dashboard);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentResponseDto>> GetById(int id)
    {
        var incident = await _incidentService.GetIncidentAsync(id);

        if (incident == null)
        {
            return NotFound();
        }

        return Ok(incident);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<List<IncidentResponseDto>>> GetByStatus(IncidentStatus status)
    {
        var incidentList = await _incidentService.GetIncidentByStatusAsync(status);

        if (incidentList == null)
        {
            return NotFound();
        }

        return Ok(incidentList);
    }

    [HttpPost]
    public async Task<ActionResult<IncidentResponseDto>> Create(CreateIncidentRequestDto incident)
    {
        var createdIncident = await _incidentService.CreateIncidentAsync(incident);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdIncident.Id },
            createdIncident);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateIncidentRequestDto incident)
    {
        var updated = await _incidentService.UpdateIncidentAsync(id, incident);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingIncident = await _incidentService.GetIncidentAsync(id);

        if (existingIncident == null)
        {
            return NotFound();
        }

        await _incidentService.DeleteIncidentAsync(id);

        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateIncidentStatusRequestDto request)
    {
        var updated = await _incidentService.UpdateIncidentStatusAsync(id, request);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }
}