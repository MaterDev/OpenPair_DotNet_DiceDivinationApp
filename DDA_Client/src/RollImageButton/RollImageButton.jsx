import PropTypes from 'prop-types'
import axios from "axios";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { getAllDiceSpreads } from "../_utils/get.requests.js";

const RollImageButton = ({ id, setDalle3ImageUrlState }) => {
  const [isLoading, setIsLoading] = useState(false);
  const dispatch = useDispatch();
  
  const createDalle3Image = async () => {
    if (id === undefined) {
      console.error("id is undefined");
      return;
    }

    setIsLoading(true);
    //! API request to create the image
    axios
      .post(`/api/createDalle3/${id}`)
      .then((response) => {
        console.log("Response from createDalle3Image:", response.data.imageUrl);
        // Example reesponse: 
        // {
        //   "imageUrl": "/storage/images/diceSpreadRolledImages/f4297d06a19d41c1b32f14451b432547_18.jpg"
        // }
        const randomNumber = Math.floor(Math.random() * 1000);
        setDalle3ImageUrlState(`${response.data.imageUrl}?v=${randomNumber}`);
      })
      .then(() => {
        
        //! After the image is created, we need to update the state of the app
        getAllDiceSpreads()
          .then((data) => {
            console.log("Data from getAllDiceSpreads:", data)
            dispatch({ type: "LOAD_STATE", payload: data });
          })
          .catch((error) =>
            console.error("Error createDalle3Image(), getAllDiceSpreads:", error)
          );
        })

      .catch((error) => {
        console.error("Error createDalleUImage:", error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  };

  return (
    <button
      className="spreadImageBtn"
      id={`spreadImageBtn-${id}`}
      data-cardid={id}
      onClick={createDalle3Image}
      disabled={isLoading}
    >
      {isLoading ? "Rendering..." : "Roll Image"}
    </button>
  );
};

RollImageButton.propTypes = {
  id: PropTypes.number.isRequired,
  setDalle3ImageUrlState: PropTypes.func.isRequired,
}

export default RollImageButton;
