using Production_Incident_Assistant.Core.Entities;

namespace Production_Incident_Assistant.Core.DTOs
{
    public class UpdateIncidentStatusRequestDto
    {
        public IncidentStatus Status { get; set; } = IncidentStatus.Open;
    }
}