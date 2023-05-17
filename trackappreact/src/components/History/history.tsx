import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IndentTd, ItemsList, SWL, SWR, Tr } from "../CurrentList/curentlist";
import { StyledTitle } from "../../styles/title.styled";
import { fetchItemHistory } from "../../services/itemListService";
import { StyledWrapper } from "../../styles/wrapper.styled";
import "./history.css";

function History() {
  const { productId } = useParams();
  const [lists, setLists] = useState<ItemsList[]>([]);

  useEffect(() => {}, []);

  useEffect(() => {
    if (!productId) return;
    fetchItemHistory(parseInt(productId), setLists);
  }, [productId]);

  console.log(lists);
  return (
    <StyledWrapper>
      <StyledTitle>Item history</StyledTitle>
      {lists.map((rows: any) => {
        return (
          <>
            <h5>List of {rows.monthOfYear}</h5>
            <table className="table table-borderless table-sm">
              <tbody>
                {rows.items.map((object: any) => (
                  <tr>
                    {object.items.map((row: any) => (
                      <Tr $crossedOff={row.crossedOff}>
                        <IndentTd>{row.name}</IndentTd>
                        <SWR>{row.quantity}</SWR>
                        <SWL>{row.unit}</SWL>
                      </Tr>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          </>
        );
      })}
    </StyledWrapper>
  );
}

export default History;
