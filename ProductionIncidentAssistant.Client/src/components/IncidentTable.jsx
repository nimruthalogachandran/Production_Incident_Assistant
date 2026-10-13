import SeverityBadge from "./SeverityBadge";
import StatusBadge from "./StatusBadge";

function IncidentTable({ incidents }) {
  return (
    <div className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">

      <div className="border-b border-slate-200 px-6 py-5">
        <h3 className="text-lg font-semibold text-slate-900">
          Recent Incidents
        </h3>

        <p className="mt-1 text-sm text-slate-500">
          Latest production incidents reported by the system
        </p>
      </div>

      <div className="overflow-x-auto">

        <table className="w-full text-left">

          <thead className="bg-slate-50">
            <tr>
              <th className="px-6 py-4 text-xs font-semibold uppercase tracking-wider text-slate-500">
                Incident
              </th>

              <th className="px-6 py-4 text-xs font-semibold uppercase tracking-wider text-slate-500">
                Severity
              </th>

              <th className="px-6 py-4 text-xs font-semibold uppercase tracking-wider text-slate-500">
                Status
              </th>

              <th className="px-6 py-4 text-xs font-semibold uppercase tracking-wider text-slate-500">
                Created
              </th>
            </tr>
          </thead>

          <tbody className="divide-y divide-slate-100">

            {incidents.map((incident) => (
              <tr
                key={incident.id}
                className="transition hover:bg-slate-50"
              >

                <td className="px-6 py-4">
                  <p className="font-medium text-slate-900">
                    {incident.title}
                  </p>

                  <p className="mt-1 max-w-md truncate text-sm text-slate-500">
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

          </tbody>

        </table>

      </div>

    </div>
  );
}

export default IncidentTable;