import React, { useEffect, useState } from "react";
import { StyledWrapper } from "../../styles/wrapper.styled";
import { FetchRepairs } from "../../services/repairService";

function Repair() {
  const [repairs, setRepairs] = useState();
  useEffect(() => {
    FetchRepairs().then(setRepairs);
  }, []);
  return (
    <StyledWrapper>
      <table>
        <thead>
          <th>Description</th>
          <th>Priority</th>
          <th>Repaired</th>
          <th>Details</th>
        </thead>
      </table>
      <tbody></tbody>
    </StyledWrapper>
  );
}

export default Repair;
