import React, { useEffect, useState } from "react";
import {
  FetchUserPurchases,
  FetchUserRequests,
} from "../../services/userService";
import { StyledWrapper } from "../../styles/wrapper.styled";
import { StyledTitle } from "../../styles/title.styled";

function UserStats() {
  const [userPurchases, setPurchases] = useState([]);
  const [userRequests, setRequests] = useState([]);
  useEffect(() => {
    FetchUserRequests(5).then(setRequests);
    FetchUserPurchases(5).then(setPurchases);
  }, []);
  return (
    <StyledWrapper>
      <h3>Users with the most purchases</h3>
      <table className="table table-borderless table-sm">
        <thead>
          <th>Name</th>
          <th>Number of purchases</th>
        </thead>
        <tbody>
          {Object.keys(userPurchases).map((key, index) => (
            <tr>
              <td>{key}</td>
              <td>{index}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <h3>Users with the most requests</h3>
      <table className="table table-borderless table-sm">
        <thead>
          <th>Name</th>
          <th>Number of purchases</th>
        </thead>
        <tbody>
          {Object.keys(userRequests).map((key, index) => (
            <tr>
              <td>{key}</td>
              <td>{index}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </StyledWrapper>
  );
}

export default UserStats;
