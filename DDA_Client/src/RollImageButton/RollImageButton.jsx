import axios from "axios";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { getAllDiceSpreads } from "../_utils/get.requests.js";

const RollImageButton = ({ id }) => {
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
        
        //! After the image is created, we need to update the state of the app
        getAllDiceSpreads()
          .then((data) => {
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

export default RollImageButton;
