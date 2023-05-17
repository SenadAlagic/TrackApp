import { useNavigate } from "react-router-dom";
import { StyledWrapper } from "../../styles/wrapper.styled";
import styled from "styled-components";

export interface ItemsList {
  id: number;
  quantity: string;
  dateCreated: Date;
  dateModified: Date;
  itemId: number;
  listId: number;
}

interface Props {
  items: ItemsList[];
  details: boolean;
  totalPrice: number;
}

const CurrentList = ({ items, details, totalPrice }: Props) => {
  const navigate = useNavigate();

  function toRestock(id: number) {
    navigate(`/restock/${id}`);
  }
  function toHistory(id: number) {
    navigate(`/history/${id}`);
  }

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
                  <Tr $crossedOff={rows.crossedOff}>
                    <IndentTd onClick={() => toHistory(rows.itemId)}>
                      {rows.name}
                    </IndentTd>
                    <SWR>{rows.quantity}</SWR>
                    <SWL>{rows.unit}</SWL>
                    {rows.crossedOff ? null : (
                      <SWR onClick={() => toRestock(rows.itemId)}>Restock</SWR>
                    )}
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

export const Tr = styled.tr<{ $crossedOff: boolean }>`
  &:hover {
    background-color: rgb(238, 238, 243);
  }
  text-decoration: ${(props) =>
    props.$crossedOff ? `line-through 2px solid black;` : `none;`};
`;
export const IndentTd = styled.td`
  padding-left: 2.5em !important;
`;
export const SWR = styled.td`
  width: 5%;
  text-align: right;
`;
export const SWL = styled.td`
  width: 5%;
  text-align: left;
`;
