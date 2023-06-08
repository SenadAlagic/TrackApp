import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList, { ItemsList } from "../CurrentList/curentlist";
import CustomModal from "../Modal/modal";
import { StyledTitle } from "../../styles/title.styled";
import styled from "styled-components";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/itemListService";
import {
  fetchCurrentWorkingList,
  fetchTotalPrice,
} from "../../services/listService";
import NewItem from "../NewItem/newitem";
import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";

function Home() {
  const [items, setItems] = useState<ItemsList[]>([]);
  const [currentListId, setCurrentList] = useState(0);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    fetchCurrentWorkingList().then(setCurrentList);
  }, []);

  useEffect(() => {
    if (!currentListId) return;
    fetchData(currentListId, true).then(setItems);
    fetchTotalPrice(currentListId).then(setTotalPrice);
  }, [currentListId]);

  const addItem = () => {
    fetchData(currentListId, true).then(setItems);
  };

  function exportToPDF() {
    const table = document.getElementById("table");
    const doc = new jsPDF();
    const columns: any = [];
    const data: any = [];

    if (!table) return;
    const headers = table.querySelectorAll("thead th");
    headers.forEach((header) => {
      columns.push(header.innerHTML);
    });

    const rows = table.querySelectorAll("tbody tr");
    rows.forEach((row) => {
      const rowData: any = [];
      const cells = row.querySelectorAll("td");
      if (cells[1].innerText !== "") {
        cells.forEach((cell) => {
          rowData.push(cell.innerText);
        });
        data.push(rowData);
      }
    });
    console.log(data);
    // Generate the PDF
    autoTable(doc, { head: [columns], body: data });

    // Save the PDF file
    doc.save("table.pdf");
  }
  return (
    <>
      <div className="dashboard">
        <StyledTitle>Dashboard</StyledTitle>
        <CurrentList items={items} details={false} totalPrice={totalPrice} />

        <StyledDiv>
          <CustomModal
            modalTitle="Add to list"
            modalButtonTitle="Add a new item to list"
          >
            <AddNewItem callback={addItem} />
          </CustomModal>
        </StyledDiv>
        <br />

        <StyledDiv>
          <CustomModal
            modalTitle="Add a new item to database"
            modalButtonTitle="Add a new item to database"
          >
            <NewItem />
          </CustomModal>
        </StyledDiv>
        <br />

        <StyledDiv>
          <button className="btn btn-secondary" onClick={exportToPDF}>
            Export to PDF
          </button>
        </StyledDiv>

        {/* <StyledTitle>Previous months</StyledTitle> */}
        {/* <PreviousLists /> */}
      </div>
    </>
  );
}

export default Home;

export const StyledDiv = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const HalvedModal = styled(CustomModal)`
  width: 50%;
`;
