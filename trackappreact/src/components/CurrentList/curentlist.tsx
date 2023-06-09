import { useNavigate } from "react-router-dom";
import { StyledWrapper } from "../../styles/wrapper.styled";
import styled from "styled-components";
import { deleteFromList } from "../../services/itemListService";
import { ReactComponent as IconDelete } from "../../assets/icon_delete.svg";
import { ReactComponent as IconBuy } from "../../assets/icon_buy.svg";
import CustomModal from "../Modal/modal";
import Confirm from "../Confirm/confirm";
import "./currentlist.css";

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
  function toDelete(id: number) {
    // if (window.confirm("Are you sure you want to delete this item?") === true) {
    //   deleteFromList(id);
    // }
    deleteFromList(id);
  }
  return (
    <>
      <StyledWrapper id="wrapper" className="CurrentList">
        <h1>{!items.length && <p>No items found</p>}</h1>
        <table id="table" className="table table-borderless table-sm">
          <Thead>
            <th>Ime</th>
            <th>Kolicina</th>
            <th>Mjera</th>
            <th></th>
          </Thead>
          <tbody>
            {items.map((item: any) => (
              <>
                <tr>
                  <Td>{item.categoryName}</Td>
                  <td></td>
                  <td></td>
                </tr>
                {item.items.map((rows: any) => (
                  <Tr $crossedOff={rows.crossedOff}>
                    <IndentTd onClick={() => toHistory(rows.itemId)}>
                      {rows.name}
                    </IndentTd>
                    <SmallWidthRight>{rows.quantity}</SmallWidthRight>
                    <SmallWidthLeft>{rows.unit}</SmallWidthLeft>
                    {rows.crossedOff ? null : (
                      <>
                        <SmallWidthRight onClick={() => toRestock(rows.itemId)}>
                          <IconBuy />
                        </SmallWidthRight>
                        {/* <SmallWidthRight onClick={() => toDelete(rows.itemId)}>
                          <IconDelete />
                        </SmallWidthRight> */}
                        {
                          <SmallWidthLeft>
                            <CustomModal
                              modalButtonTitle=""
                              modalTitle="Delete record?"
                              icon={<IconDelete />}
                            >
                              <Confirm onDelete={() => toDelete(rows.itemId)} />
                            </CustomModal>
                          </SmallWidthLeft>
                        }
                      </>
                    )}
                  </Tr>
                ))}
              </>
            ))}
            {details && (
              <tr>
                <th>Total price:</th>
                <th>{totalPrice}</th>
                <th>KM</th>
              </tr>
            )}
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
    cursor: pointer;
  }
  text-decoration: ${(props) =>
    props.$crossedOff ? `line-through 2px solid black;` : `none;`};
`;
export const IndentTd = styled.td`
  padding-left: 2.5em !important;
`;
export const SmallWidthRight = styled.td`
  width: 5%;
  text-align: right;
`;
export const SmallWidthLeft = styled.td`
  width: 5%;
  text-align: left;
`;
const Thead = styled.thead`
  display: none;
`;
const Td = styled.td`
  font-weight: bold;
`;
