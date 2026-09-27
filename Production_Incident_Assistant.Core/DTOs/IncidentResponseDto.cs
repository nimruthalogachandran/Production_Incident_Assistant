namespace Production_Incident_Assistant.Core.DTOs
{
    public class IncidentResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}