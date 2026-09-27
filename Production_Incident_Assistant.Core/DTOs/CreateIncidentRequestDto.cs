namespace Production_Incident_Assistant.Core.DTOs
{
    public class CreateIncidentRequestDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;
    }
}