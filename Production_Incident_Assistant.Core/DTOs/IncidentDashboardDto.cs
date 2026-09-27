using System;
using System.Collections.Generic;
using System.Text;

namespace Production_Incident_Assistant.Core.DTOs
{
        public class IncidentDashboardDto
        {
            public int TotalIncidents { get; set; }

            public int OpenIncidents { get; set; }

            public int InvestigatingIncidents { get; set; }

            public int ResolvedIncidents { get; set; }

            public int HighSeverityIncidents { get; set; }

            public int CriticalSeverityIncidents { get; set; }
        }
}
