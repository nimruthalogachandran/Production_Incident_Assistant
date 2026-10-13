function StatusBadge({ status }) {
  const styles = {
    Open: "bg-blue-100 text-blue-700",
    Investigating: "bg-purple-100 text-purple-700",
    Resolved: "bg-green-100 text-green-700",
  };

  return (
    <span
      className={`rounded-full px-3 py-1 text-xs font-semibold ${
        styles[status] || "bg-slate-100 text-slate-700"
      }`}
    >
      {status}
    </span>
  );
}

export default StatusBadge;