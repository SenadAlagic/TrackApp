import { ReactNode, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IndentTd, ItemsList, SWL, SWR, Tr } from "../CurrentList/curentlist";
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
      {lists.map((rows: any) => {
        return (
          <>
            <h5>List of {rows.monthOfYear}</h5>
            <table className="table table-borderless table-sm">
              {rows.items.map((object: any) => (
                <tbody>
                  {object.items.map((row: any) => (
                    <Tr $crossedOff={row.crossedOff}>
                      <IndentTd>{row.name}</IndentTd>
                      <SWR>{row.quantity}</SWR>
                      <SWL>{row.unit}</SWL>
                    </Tr>
                  ))}
                </tbody>
              ))}
            </table>
          </>
        );
      })}
      <br></br>
      <h3>druga metoda</h3>
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

const Quantity = styled(IndentTd)`
  width: 5%;
`;
const Unit = styled(SWL)`
  width: 30%;
`;
const Price = styled(SWL)`
  width: 30%;
`;

const Date = styled.td`
  width: 30%;
  text-align: center;
`;
