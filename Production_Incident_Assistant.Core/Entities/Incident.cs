using System;
using System.Collections.Generic;
using System.Text;

namespace Production_Incident_Assistant.Core.Entities
{
    public class Incident
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;

        public IncidentStatus Status { get; set; } = IncidentStatus.Open;

        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
