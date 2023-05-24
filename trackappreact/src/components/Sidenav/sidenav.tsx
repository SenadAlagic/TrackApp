import React from "react";
import { Nav } from "react-bootstrap";

function Sidenav() {
  return (
    <Nav className="flex-column">
      <Nav.Link href="#">Link1</Nav.Link>
      <Nav.Link href="#">Link2</Nav.Link>
      <Nav.Link href="#">Link3</Nav.Link>
    </Nav>
  );
}

export default Sidenav;
