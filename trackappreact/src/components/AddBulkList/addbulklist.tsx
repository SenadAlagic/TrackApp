import React, { useEffect, useState } from "react";
import AddBulkRow from "../AddBulkRow/addbulkrow";
import styled from "styled-components";
import { StyledTitle } from "../../styles/title.styled";
import { restockInBulk } from "../../services/itemListService";

export interface Restock {
  itemId: number;
  quantity: number;
  price: number;
}

function AddBulk() {
  const [rows, setRows] = useState<Restock[]>([]);

  function addRows() {
    const newItem: Restock = {
      itemId: 0,
      quantity: 1,
      price: 0,
    };
    setRows([...rows, newItem]);
  }

  useEffect(() => {
    addRows();
  }, []);

  async function restock() {
    await restockInBulk(rows);
  }

  return (
    <Wrapper>
      <StyledTitle>Add in bulk</StyledTitle>
      <ButtonDiv id="buttonDiv">
        <button className="btn btn-primary" onClick={addRows}>
          Add new
        </button>
      </ButtonDiv>
      <ContentDiv id="contentDiv">
        {rows.map((item, index) => (
          <>
            <AddBulkRow restock={item}></AddBulkRow>
            <br />
          </>
        ))}
        <button className="btn btn-primary" onClick={restock}>
          Restock
        </button>
      </ContentDiv>
    </Wrapper>
  );
}

export default AddBulk;

const Row = styled(AddBulkRow)`
  background-color: red;
`;
const Wrapper = styled.div`
  width: 60%;
  margin-left: auto;
  margin-right: auto;
  display: flex;
  flex-wrap: wrap;
`;
const ButtonDiv = styled.div`
  text-align: right;
  width: 100%;
  margin-bottom: 10px;
`;
const ContentDiv = styled.div`
  width: 100%;
`;
