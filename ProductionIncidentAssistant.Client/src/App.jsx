
import { useState } from "react";
import Dashboard from "./components/Dashboard";
import Incidents from "./components/Incidents";

function App() {
  const [currentPage, setCurrentPage] = useState("Dashboard");

  return (
    <>
      {currentPage === "Dashboard" ? (
        <Dashboard onNavigate={setCurrentPage} />
      ) : (
        <Incidents onNavigate={setCurrentPage} />
      )}
    </>
  );
}

export default App;
