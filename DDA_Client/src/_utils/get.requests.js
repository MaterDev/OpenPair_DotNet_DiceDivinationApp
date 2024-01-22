import axios from "axios";

export const getAllDiceSpreads = async (setDiceSpreadContent) => {
    return axios
      .get("/api/getAllDiceSpreads")
      .then((response) => {
        return response.data
      })
      .catch((error) => console.error("Error fetching dice rolls:", error));
  };