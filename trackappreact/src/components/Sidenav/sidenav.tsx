import { useEffect, useState } from "react";
import { Nav } from "react-bootstrap";
import "../Sidenav/sidenav.css";
import { Link } from "react-router-dom";
import { ReactComponent as MenuIcon } from "../../assets/burger_menu_icon.svg";

function Sidenav() {
  const [isOpen, setIsOpen] = useState(false);
  useEffect(() => {
    const currentState =
      localStorage.getItem("sidebar") === "true" ? true : false;
    setIsOpen(currentState);
  }, []);

  const toggle = () => {
    setIsOpen(!isOpen);
    const currentState = isOpen ? "false" : "true";
    localStorage.setItem("sidebar", currentState);
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
                Restock in bulk
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
            {/* <li className="nav-item">
              <div className="nav-link" onClick={openChat}>
                Open chat
              </div>
            </li> */}
          </ul>
        </Nav>
        <div className="toggle-button" onClick={toggle}>
          <MenuIcon />
        </div>
      </div>
    </>
  );
}

export default Sidenav;
