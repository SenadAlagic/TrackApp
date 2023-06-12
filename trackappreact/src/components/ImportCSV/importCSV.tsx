import { ChangeEvent, useState } from "react";
import { StyledWrapper } from "../../styles/wrapper.styled";
import { StyledTitle } from "../../styles/title.styled";
import { IndentTd } from "../CurrentList/curentlist";
import { SmallWidthRight } from "../CurrentList/curentlist";
import { SmallWidthLeft } from "../CurrentList/curentlist";
import styled from "styled-components";

function ImportCSV() {
  const [isVisible, setVisible] = useState(true);
  const [array, setArray] = useState([]);

  const fileReader = new FileReader();

  const handleOnChange = (e: ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) return;
    if (e.target.files[0]) {
      fileReader.onload = function (event: any) {
        const text = event.target.result;
        csvFileToArray(text);
      };
      fileReader.readAsText(e.target.files[0]);
    }
  };

  const csvFileToArray = (string: any) => {
    debugger;
    const csvHeader = string.slice(0, string.indexOf("\n")).split(",");
    const csvRows = string.slice(string.indexOf("\n") + 1).split("\n");

    const array = csvRows.map((i: any) => {
      const values = i.split(",");
      const obj = csvHeader.reduce((object: any, header: any, index: any) => {
        object[header] = values[index];
        return object;
      }, {});
      return obj;
    });
    setVisible(false);
    setArray(array);
  };

  const headerKeys = Object.keys(Object.assign({}, ...array));
  console.log(array);
  return (
    <StyledWrapper>
      <div style={{ textAlign: "center" }}>
        <StyledTitle>Import a .csv file</StyledTitle>
        <br />
        <form>
          {isVisible && (
            <input
              className="form-control"
              type={"file"}
              id={"csvFileInput"}
              accept={".csv"}
              onChange={handleOnChange}
            />
          )}
        </form>

        <br />

        <table className="table table-borderless table-sm">
          <thead>
            <tr key={"header"}>
              {headerKeys.map((key) => (
                <th>{key}</th>
              ))}
            </tr>
          </thead>

          <tbody>
            {array.map((item: any) => (
              <Tr $category={item.Unit}>
                <LocalTd $indent={item.Unit}>{item.Name}</LocalTd>
                <SmallWidthRight>{item.Quantity}</SmallWidthRight>
                <SmallWidthLeft>{item.Unit}</SmallWidthLeft>
              </Tr>
            ))}
          </tbody>
        </table>
      </div>
    </StyledWrapper>
  );
}

export default ImportCSV;

const LocalTd = styled(IndentTd)<{ $indent: string }>`
  text-align: left;
  padding-left: ${(props) =>
    props.$indent ? "2.5em !important" : "0px !important"};>
`;
const Tr = styled.tr<{ $category: string }>`
  font-weight: ${(props) => (props.$category ? "none" : "bold")};
`;
