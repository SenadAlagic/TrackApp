import { useState } from "react";
import { Nav } from "react-bootstrap";
import "../Sidenav/sidenav.css";

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
              <Nav.Link href="/" className="nav-link" aria-current="page">
                Home
              </Nav.Link>
            </li>
            <li className="nav-item">
              <Nav.Link href="details" className="nav-link" aria-current="page">
                Details
              </Nav.Link>
            </li>
            <li className="nav-item">
              <Nav.Link
                href="/addBulk"
                className="nav-link"
                aria-current="page"
              >
                Add in bulk
              </Nav.Link>
            </li>
            <li className="nav-item">
              <Nav.Link
                href="/addBulk"
                className="nav-link"
                aria-current="page"
              >
                Add a new item to database
              </Nav.Link>
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
