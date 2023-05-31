import { useState } from "react";
import { useCache } from "../../services/cacheService";
import { appSettings } from "../../site";

interface GraphProps {
  ReactFC: any;
  itemId: number;
}

function Graph({ ReactFC, itemId }: GraphProps) {
  const [checked, setChecked] = useState(false);
  let dataSource: any = [];
  //const[data,setData]=useState();
  const { status, error, data } = useCache(
    `${appSettings.apiUrl}/ItemList/GetForDiagram?itemId=${itemId}&displayInWeeks=${checked}`
  );

  function changeChecked(state: boolean) {
    setChecked(state);
  }
  if (data.length > 3) dataSource = data;
  var myDataSource = {
    chart: {
      caption: "Consumation through time",
      subCaption: "",
      canvasBgAlpha: "0",
      bgColor: "#ffffff",
      bgAlpha: "70",
      baseFont: "Roboto",
      baseFontSize: "14",
      showAlternateVGridColor: "1",
      alternateVGridAlpha: "5",
      labelFontSize: "15",
      captionFontSize: "20",
      subCaptionFontSize: "16",
      toolTipColor: "#000000",
      toolTipBgColor: "#ffffff",
      toolTipAlpha: "90",
      captionFontBold: "0",
      subCaptionFontBold: "0",
      paletteColors: "#8E24AA",
      valueFontSize: "13",
      valueFontBold: "0",
      animation: "0",
      divLineAlpha: "15",
      divLineDashed: "0",
      plotFillAlpha: "90",
      theme: "ocean",
    },
    data: dataSource,
  };

  var barChartConfigs = {
    id: "line-chart",
    renderAt: "chart-container",
    type: "line",
    width: "100%",
    height: 400,
    dataFormat: "json",
    dataSource: myDataSource,
  };

  return (
    <div>
      <ReactFC {...barChartConfigs} />
      <div className="form-check">
        <input
          className="form-check-input"
          type="radio"
          name="flexRadioDefault"
          id="flexRadioDefault1"
          checked={checked === false}
          onChange={() => changeChecked(false)}
        />
        <label className="form-check-label" htmlFor="flexRadioDefault1">
          Months
        </label>
      </div>
      <div className="form-check">
        <input
          className="form-check-input"
          type="radio"
          value="weeks"
          name="flexRadioDefault"
          id="flexRadioDefault2"
          checked={checked === true}
          onChange={() => changeChecked(true)}
        />
        <label className="form-check-label" htmlFor="flexRadioDefault2">
          Weeks
        </label>
      </div>
    </div>
  );
}

export default Graph;
