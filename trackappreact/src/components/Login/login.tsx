import styled from "styled-components";
import { StyledWrapper } from "../../styles/wrapper.styled";
import "./login.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {
  const [name, setName] = useState("");
  const navigate = useNavigate();
  function Login() {
    localStorage.setItem("user", name);
    navigate("/");
  }
  return (
    <LocalWrapper>
      <br />
      <label>Your Name: </label>
      <input
        className="form-control control"
        placeholder="Your name"
        onChange={(e) => setName(e.target.value)}
      ></input>
      <button className="btn btn-primary" onClick={Login}>
        Login
      </button>
    </LocalWrapper>
  );
}

export default Login;

export const LocalWrapper = styled(StyledWrapper)`
  display: flex;
  flex-wrap: wrap;
`;
