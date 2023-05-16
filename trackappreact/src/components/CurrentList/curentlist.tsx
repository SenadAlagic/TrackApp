import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { StyledWrapper } from "../../styles/wrapper.styled";
import styled from "styled-components";
import { setPriority } from "os";

export interface ItemsList {
  id: number;
  quantity: string;
  dateCreated: Date;
  dateModified: Date;
  itemId: number;
  listId: number;
}

interface Props {
  details: boolean;
}

const CurrentList = ({ details }: Props) => {
  const [items, setItems] = useState<ItemsList[]>([]);
  const [currentListId, setCurrentListId] = useState(0);
  const [totalPrice, setTotalPrice] = useState(0);
  const navigate = useNavigate();

  function toRestock(id: number) {
    navigate(`/restock/${id}`);
  }

  useEffect(() => {
    getCurrentWorkingList(setCurrentListId);
  }, []);

  useEffect(() => {
    if (!currentListId) return;
    fetchData(currentListId, details, setItems);
    fetchTotalPrice(currentListId, setTotalPrice);
  }, [currentListId]);

  return (
    <>
      <StyledWrapper id="wrapper" className="CurrentList">
        <h1>{items.length === 0 && <p>No items found</p>}</h1>
        <table className="table table-borderless table-sm">
          <tbody>
            {items.map((item: any) => (
              <>
                <tr>
                  <th>{item.categoryName}</th>
                  <th></th>
                  <th></th>
                </tr>
                {item.items.map((rows: any) => (
                  <Tr onClick={() => toRestock(rows.itemId)}>
                    <IndentTd>{rows.name}</IndentTd>
                    <SWR>{rows.quantity}</SWR>
                    <SWL>{rows.unit}</SWL>
                  </Tr>
                ))}
              </>
            ))}
            {details ? (
              <tr>
                <th>Total price:</th>
                <th>{totalPrice}</th>
                <th>KM</th>
              </tr>
            ) : null}
          </tbody>
        </table>
      </StyledWrapper>
    </>
  );
};

export default CurrentList;

async function getCurrentWorkingList(
  set: React.Dispatch<React.SetStateAction<number>>
) {
  try {
    const res = await fetch(
      `https://localhost:7280/List/GetCurrentWorkingList`
    );
    if (!res.ok) return;
    const data = await res.json();
    set(data.id);
    return data.id;
  } catch (error) {
    console.log(error);
  }
}

async function fetchData(
  listId: number,
  filter: boolean,
  set: React.Dispatch<React.SetStateAction<ItemsList[]>>
) {
  if (!listId) return;
  try {
    const res = await fetch(
      `https://localhost:7280/ItemList/GetByList?id=${listId}&filterByQuantity=${!filter}`
    );
    if (!res.ok) return;
    const data = await res.json();
    set(data);
  } catch (error) {
    console.log(error);
  }
}

async function fetchTotalPrice(
  listId: number,
  set: React.Dispatch<React.SetStateAction<number>>
) {
  if (listId) return;
  try {
    const res = await fetch(`https://localhost:7280/List/GetList?id=${listId}`);
    if (!res.ok) return;
    const data = await res.json();
    set(data.totalPrice);
    return data.totalPrice;
  } catch (error) {
    console.log(error);
  }
}

const Tr = styled.tr`
  &:hover {
    background-color: rgb(232, 237, 234);
  }
`;
const IndentTd = styled.td`
  padding-left: 2.5em !important;
`;
const SWR = styled.td`
  width: 5%;
  text-align: right;
`;
const SWL = styled.td`
  width: 5%;
  text-align: left;
`;
