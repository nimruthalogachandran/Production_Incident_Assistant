import { useEffect, useState } from "react";
import StatCard from "./StatCard";
import IncidentTable from "./IncidentTable";
import SeverityChart from "./SeverityChart";

function Dashboard({ onNavigate }) {
  const [dashboard, setDashboard] = useState(null);
  const [incidents, setIncidents] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5089/api/Incidents/dashboard")
      .then((response) => response.json())
      .then((data) => {
        setDashboard(data);
      });
      fetch("http://localhost:5089/api/Incidents")
  .then((response) => response.json())
  .then((data) => {
    setIncidents(data);
  });
  }, []);

  if (!dashboard) {
    return <p>Loading dashboard...</p>;
  }

  return (
    <div className="min-h-screen bg-slate-100">

      {/* Sidebar */}
      <aside className="fixed left-0 top-0 h-screen w-64 bg-slate-900 text-white">

        <div className="border-b border-slate-700 p-6">
          <h1 className="text-xl font-bold">
            Incident Assistant
          </h1>

          <p className="mt-1 text-sm text-slate-400">
            Production Operations
          </p>
        </div>

        <nav className="p-4">

          <button
    onClick={() => onNavigate("Dashboard")}
    className="w-full rounded-lg bg-slate-800 px-4 py-3 text-left"
  >
    Dashboard
  </button>

  <button
    onClick={() => onNavigate("Incidents")}
    className="w-full rounded-lg px-4 py-3 text-left text-slate-400 hover:bg-slate-800 hover:text-white"
  >
    Incidents
  </button>

  <div className="px-4 py-3 text-slate-400">Analytics</div>
  <div className="px-4 py-3 text-slate-400">Settings</div>

        </nav>

      </aside>

      {/* Main Content */}
      <main className="ml-64 min-h-screen">

        {/* Header */}
        <header className="flex items-center justify-between border-b border-slate-200 bg-white px-8 py-5">

          <div>
            <h2 className="text-2xl font-bold text-slate-900">
              Dashboard
            </h2>

            <p className="mt-1 text-sm text-slate-500">
              Monitor and manage production incidents
            </p>
          </div>

          <div className="flex items-center gap-3">

            <span className="h-2.5 w-2.5 rounded-full bg-green-500"></span>

            <span className="text-sm font-medium text-slate-600">
              System Operational
            </span>

          </div>

        </header>

        {/* Dashboard Content */}
        <section className="p-8">

          <div className="grid grid-cols-1 gap-6 md:grid-cols-2 xl:grid-cols-4">

  <StatCard
    title="Total Incidents"
    value={dashboard.totalIncidents}
    description="All reported incidents"
  />

  <StatCard
    title="Open Incidents"
    value={dashboard.openIncidents}
    description="Currently awaiting resolution"
  />

  <StatCard
    title="Investigating"
    value={dashboard.investigatingIncidents}
    description="Currently being investigated"
  />

  <StatCard
    title="Resolved"
    value={dashboard.resolvedIncidents}
    description="Successfully resolved"
  />

</div>
<div className="mt-8">

  <div className="mb-4">
    <h3 className="text-lg font-semibold text-slate-900">
      Severity Overview
    </h3>

    <p className="text-sm text-slate-500">
      Monitor incidents requiring immediate attention
    </p>
  </div>

  <div className="grid grid-cols-1 gap-6 md:grid-cols-2">

    <StatCard
      title="High Severity"
      value={dashboard.highSeverityIncidents}
      description="High priority incidents"
    />

    <StatCard
      title="Critical Severity"
      value={dashboard.criticalSeverityIncidents}
      description="Requires immediate attention"
    />

  </div>
  <div className="mt-8">
  <SeverityChart dashboard={dashboard} />
</div>
  <div className="mt-8">
  <IncidentTable incidents={incidents} />
</div>

</div>

        </section>

      </main>

    </div>
  );
}

export default Dashboard;