import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";

function SeverityChart({ dashboard }) {
  const data = [
    {
      name: "Critical",
      value: dashboard.criticalSeverityIncidents,
    },
    {
      name: "High",
      value: dashboard.highSeverityIncidents,
    },
  ];

  return (
    <div className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">

      <div className="mb-6">
        <h3 className="text-lg font-semibold text-slate-900">
          Severity Distribution
        </h3>

        <p className="mt-1 text-sm text-slate-500">
          Current high-priority incidents
        </p>
      </div>

      <div className="h-64">

        <ResponsiveContainer width="100%" height="100%">

          <PieChart>

            <Pie
              data={data}
              dataKey="value"
              nameKey="name"
              cx="50%"
              cy="50%"
              outerRadius={90}
              label
            >
              <Cell />
              <Cell />
            </Pie>

            <Tooltip />

            <Legend />

          </PieChart>

        </ResponsiveContainer>

      </div>

    </div>
  );
}

export default SeverityChart;