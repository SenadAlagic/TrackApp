import { ReactNode, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {
  IndentTd,
  ItemsList,
  SmallWidthLeft,
  SmallWidthRight,
  Tr,
} from "../CurrentList/curentlist";
import { StyledTitle } from "../../styles/title.styled";
import { fetchItemHistory } from "../../services/itemListService";
import { StyledWrapper } from "../../styles/wrapper.styled";
import "./history.css";
import ItemService from "../../services/itemService";
import styled from "styled-components";

export interface Purchase {
  id: number;
  itemId: number;
  itemName: string;
  dateOfPurchase: string;
  quantity: number;
  isVisible: boolean;
  unit: string;
  price: number;
}

function History() {
  const { productId } = useParams();
  const [lists, setLists] = useState<ItemsList[]>([]);
  const [purchases, setPurchases] = useState<Purchase[]>([]);

  useEffect(() => {}, []);

  useEffect(() => {
    if (!productId) return;
    fetchItemHistory(parseInt(productId), setLists);
    ItemService.fetchPurchases(parseInt(productId), setPurchases);
  }, [productId]);

  return (
    <StyledWrapper>
      <StyledTitle>Item history</StyledTitle>
      <br />
      {lists.map((rows: any) => {
        return (
          <>
            <h5>List of {FormatDateMonth(rows.monthOfYear)}</h5>
            <table className="table table-borderless table-sm">
              {rows.items.map((object: any) => (
                <tbody>
                  {object.items.map((row: any) => (
                    <Tr $crossedOff={row.crossedOff}>
                      <IndentTd>{row.name}</IndentTd>
                      <SmallWidthRight>{row.quantity}</SmallWidthRight>
                      <SmallWidthLeft>{row.unit}</SmallWidthLeft>
                    </Tr>
                  ))}
                </tbody>
              ))}
            </table>
          </>
        );
      })}
      <br></br>
      <StyledTitle>Purchase history</StyledTitle>
      <br />
      <>
        <table className="table table-borderless table-sm">
          <thead>
            <th>Quantity</th>
            <th></th>
            <th>Price</th>
            <th>Date of purchase</th>
          </thead>
          <tbody>
            {purchases.map((purchase: Purchase) => (
              <tr>
                <Quantity>{purchase.quantity}</Quantity>
                <Unit>{purchase.unit}</Unit>
                <Price>{purchase.price} KM</Price>
                <Date>{FormatDate(purchase.dateOfPurchase)}</Date>
              </tr>
            ))}
            <tr>
              <td></td>
              <th>Total price:</th>
              <td>{TotalPrice(purchases)} KM</td>
            </tr>
          </tbody>
        </table>
      </>
    </StyledWrapper>
  );
}

export default History;

function FormatDate(date: string): ReactNode {
  return date.slice(0, 10);
}
function FormatDateMonth(date: string): ReactNode {
  return date.slice(0, 7);
}

function TotalPrice(purchases: Purchase[]): ReactNode {
  let sum = 0.0;
  for (let purchase of purchases) sum = sum + purchase.price;
  return sum;
}

const Quantity = styled(IndentTd)`
  width: 5%;
`;
const Unit = styled(SmallWidthLeft)`
  width: 30%;
`;
const Price = styled(SmallWidthLeft)`
  width: 30%;
`;

const Date = styled.td`
  width: 30%;
  text-align: center;
`;
