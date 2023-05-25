import React, { useState } from "react";
import { Wrapper } from "../AddNewItem/AddNewItem";
import AddBulkRow from "../AddBulkRow/addbulkrow";

function AddBulk() {
  const [rows, setRows] = useState([]);
  return (
    <Wrapper>
      {rows.map((item, index) => (
        <AddBulkRow key={index}></AddBulkRow>
      ))}
    </Wrapper>
  );
}

export default AddBulk;
