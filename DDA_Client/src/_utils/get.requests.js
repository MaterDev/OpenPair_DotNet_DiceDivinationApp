import axios from "axios";

export const getAllDiceSpreads = (setDiceSpreadContent) => {
    axios
      .get("/api/getAllDiceSpreads")
      .then((response) => {
        console.log("getAllDice", response.data);
        setDiceSpreadContent(response.data);
      })
      .catch((error) => console.error("Error fetching dice rolls:", error));
  };