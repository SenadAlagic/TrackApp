import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Items } from "../AddNewItem/AddNewItem";
import ItemService from "../../services/itemService";
import { useNavigate } from "react-router-dom";
import { StyledTitle } from "../../styles/title.styled";
import styled from "styled-components";
import "./restock.css";
import { appSettings } from "../../site";

function Restock() {
  const { productId } = useParams();
  const [item, setItem] = useState<Items>();
  const [quantity, setQuantity] = useState();
  const [price, setPrice] = useState();
  const navigate = useNavigate();

  function changeQuantity(event: any) {
    setQuantity(event.target.value);
  }
  function changePrice(event: any) {
    setPrice(event.target.value);
  }

  const fetchItem = async () => {
    try {
      const res = await fetch(
        `${appSettings.apiUrl}/Item/GetById?id=${productId}`,
        {
          method: "GET",
          headers: {
            Accept: "application/json",
          },
        }
      );
      const data = await res.json();
      setItem(data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchItem();
  }, []);

  const logPurchase = () => {
    let body = {
      itemId: item?.id,
      listId: 1,
      quantity: parseInt(quantity || ""),
      totalPrice: parseInt(price || ""),
    };
    ItemService.restock(body);
    navigate("/details");
  };

  return (
    <>
      <Div className="restock">
        <StyledTitle>Restock</StyledTitle>
        <Input
          className="form-control control"
          value={item?.name}
          id="itemName"
        ></Input>
        <Controls className="controls">
          <Inputwidth
            className="form-control control"
            placeholder="Quantity"
            onChange={changeQuantity}
            value={quantity}
          ></Inputwidth>
          <Inputwidth
            className="form-control control"
            placeholder="Price"
            onChange={changePrice}
            value={price}
          ></Inputwidth>
        </Controls>
        <Button
          className="btn btn-primary control"
          type="button"
          onClick={() => logPurchase()}
        >
          Log purchase
        </Button>
      </Div>
    </>
  );
}

export default Restock;

const Div = styled.div`
  margin-right: auto;
  margin-left: auto;
  width: 50%;
  height: 100%;
  text-align: center;
`;
const Controls = styled.div`
  display: flex;
  flex-wrap: wrap;
`;
const Input = styled.input`
  width: 100%;
`;
const Inputwidth = styled.input`
  width: 50%;
`;
const Button = styled.button`
  width: 100%;
`;
