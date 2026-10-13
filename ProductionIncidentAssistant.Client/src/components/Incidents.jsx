
import { useEffect, useState } from "react";
import SeverityBadge from "./SeverityBadge";
import StatusBadge from "./StatusBadge";

const API_URL = "http://localhost:5089/api/Incidents";

function Incidents({ onNavigate }) {
  const [incidents, setIncidents] = useState([]);
  const [search, setSearch] = useState("");
  const [severity, setSeverity] = useState("All");
  const [status, setStatus] = useState("All");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetch(API_URL)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Unable to load incidents.");
        }
        return response.json();
      })
      .then((data) => setIncidents(data))
      .catch(() => setError("Could not load incidents. Check the API connection."))
      .finally(() => setLoading(false));
  }, []);

  const filteredIncidents = incidents.filter((incident) => {
    const query = search.toLowerCase();

    const matchesSearch =
      incident.title.toLowerCase().includes(query) ||
      incident.description.toLowerCase().includes(query);

    const matchesSeverity =
      severity === "All" || incident.severity === severity;

    const matchesStatus =
      status === "All" || incident.status === status;

    return matchesSearch && matchesSeverity && matchesStatus;
  });

  return (
    <div className="min-h-screen bg-slate-100">
      <aside className="fixed left-0 top-0 h-screen w-64 bg-slate-900 text-white">
        <div className="border-b border-slate-700 p-6">
          <h1 className="text-xl font-bold">Incident Assistant</h1>
          <p className="mt-1 text-sm text-slate-400">
            Production Operations
          </p>
        </div>

        <nav className="p-4 space-y-2">
          <button
    onClick={() => onNavigate("Dashboard")}
    className="w-full rounded-lg px-4 py-3 text-left text-slate-400 hover:bg-slate-800 hover:text-white"
  >
    Dashboard
  </button>
          <div className="rounded-lg bg-slate-800 px-4 py-3">
            Incidents
          </div>

          <div className="px-4 py-3 text-slate-400">Analytics</div>
          <div className="px-4 py-3 text-slate-400">Settings</div>
        </nav>
      </aside>

      <main className="ml-64 min-h-screen">
        <header className="border-b border-slate-200 bg-white px-8 py-5">
          <h2 className="text-2xl font-bold text-slate-900">
            Incident Management
          </h2>
          <p className="mt-1 text-sm text-slate-500">
            Search, review, and filter production incidents
          </p>
        </header>

        <section className="p-8">
          <div className="mb-6 grid grid-cols-1 gap-4 md:grid-cols-3">
            <input
              value={search}
              onChange={(event) => setSearch(event.target.value)}
              placeholder="Search title or description..."
              className="rounded-lg border border-slate-300 bg-white px-4 py-3 outline-none focus:border-blue-500"
            />

            <select
              value={severity}
              onChange={(event) => setSeverity(event.target.value)}
              className="rounded-lg border border-slate-300 bg-white px-4 py-3"
            >
              <option value="All">All severities</option>
              <option value="Low">Low</option>
              <option value="Medium">Medium</option>
              <option value="High">High</option>
              <option value="Critical">Critical</option>
            </select>

            <select
              value={status}
              onChange={(event) => setStatus(event.target.value)}
              className="rounded-lg border border-slate-300 bg-white px-4 py-3"
            >
              <option value="All">All statuses</option>
              <option value="Open">Open</option>
              <option value="Investigating">Investigating</option>
              <option value="Resolved">Resolved</option>
            </select>
          </div>

          <div className="mb-4 text-sm text-slate-500">
            Showing {filteredIncidents.length} of {incidents.length} incidents
          </div>

          {loading ? (
            <p className="text-slate-500">Loading incidents...</p>
          ) : error ? (
            <p className="rounded-lg bg-red-50 p-4 text-red-700">{error}</p>
          ) : (
            <div className="overflow-x-auto rounded-2xl border border-slate-200 bg-white shadow-sm">
              <table className="w-full text-left">
                <thead className="bg-slate-50">
                  <tr>
                    {["ID", "Incident", "Severity", "Status", "Created"].map(
                      (heading) => (
                        <th
                          key={heading}
                          className="px-6 py-4 text-xs font-semibold uppercase tracking-wider text-slate-500"
                        >
                          {heading}
                        </th>
                      )
                    )}
                  </tr>
                </thead>

                <tbody className="divide-y divide-slate-100">
                  {filteredIncidents.map((incident) => (
                    <tr key={incident.id} className="hover:bg-slate-50">
                      <td className="px-6 py-4 text-sm text-slate-500">
                        #{incident.id}
                      </td>
                      <td className="px-6 py-4">
                        <p className="font-medium text-slate-900">
                          {incident.title}
                        </p>
                        <p className="mt-1 max-w-md text-sm text-slate-500">
                          {incident.description}
                        </p>
                      </td>
                      <td className="px-6 py-4">
                        <SeverityBadge severity={incident.severity} />
                      </td>
                      <td className="px-6 py-4">
                        <StatusBadge status={incident.status} />
                      </td>
                      <td className="whitespace-nowrap px-6 py-4 text-sm text-slate-500">
                        {new Date(incident.createdAt).toLocaleDateString()}
                      </td>
                    </tr>
                  ))}

                  {!loading && !error && filteredIncidents.length === 0 && (
                    <tr>
                      <td colSpan="5" className="px-6 py-10 text-center text-slate-500">
                        No incidents match your filters.
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          )}
        </section>
      </main>
    </div>
  );
}

export default Incidents;
