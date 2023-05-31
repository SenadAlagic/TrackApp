import { ChangeEvent, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Items } from "../AddNewItem/AddNewItem";
import { useNavigate } from "react-router-dom";
import { StyledTitle } from "../../styles/title.styled";
import styled from "styled-components";
import "./restock.css";
import { appSettings } from "../../site";
import { restock } from "../../services/itemListService";

function Restock() {
  const { productId } = useParams();
  const [item, setItem] = useState<Items>();
  const [quantity, setQuantity] = useState(1);
  const [name, setName] = useState("");
  const [price, setPrice] = useState(0);
  const navigate = useNavigate();

  function changeQuantity(event: ChangeEvent<HTMLInputElement>) {
    setQuantity(parseInt(event.target.value));
  }
  function changePrice(event: ChangeEvent<HTMLInputElement>) {
    setPrice(parseInt(event.target.value));
  }
  function changeName(event: ChangeEvent<HTMLInputElement>) {
    setName(event.target.value);
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
    const body = {
      itemId: item?.itemId,
      quantity: quantity,
      totalPrice: price,
      purchasedBy: name,
    };
    restock(body);
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
            type="number"
            onChange={changeQuantity}
            value={quantity}
          ></Inputwidth>
          <Inputwidth
            className="form-control control"
            placeholder="Price"
            onChange={changePrice}
            value={price}
          ></Inputwidth>
          <Inputwidth
            className="form-control control"
            placeholder="Name"
            type="text"
            onChange={changeName}
            value={name}
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
  width: 33%;
`;
const Button = styled.button`
  width: 100%;
`;
