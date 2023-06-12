import { useState } from "react";
import { Nav } from "react-bootstrap";
import "../Sidenav/sidenav.css";
import { Link } from "react-router-dom";

function Sidenav() {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => {
    setIsOpen(!isOpen);
  };
  return (
    <>
      <div className={`sidenav ${isOpen ? "open" : ""}`}>
        <Nav className="flex-column" id="navWrapper">
          <ul className="nav nav-pills flex-column mb-auto">
            <li className="nav-item">
              <Link to="/" className="nav-link" aria-current="page">
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/details" className="nav-link" aria-current="page">
                Details
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/addBulk" className="nav-link" aria-current="page">
                Add in bulk
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/graphs" className="nav-link" aria-current="page">
                View graphs
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/userStats" className="nav-link" aria-current="page">
                Users & stats
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/repairs" className="nav-link" aria-current="page">
                Repairs
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/import" className="nav-link" aria-current="page">
                Import
              </Link>
            </li>
          </ul>
        </Nav>
        <div className="toggle-button" onClick={toggle}>
          ≡
        </div>
      </div>
    </>
  );
}

export default Sidenav;
