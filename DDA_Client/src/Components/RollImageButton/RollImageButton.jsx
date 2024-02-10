import PropTypes from 'prop-types'
import axios from "axios";
import { useDispatch } from "react-redux";
import { getAllDiceSpreads } from "../../_utils/get.requests.js";
import { Button } from "primereact/button";

const RollImageButton = ({ disabled, id, setDalle3ImageUrlState, setIsRollingImage }) => {
  
  const dispatch = useDispatch();
  
  const createDalle3Image = async () => {
    if (id === undefined) {
      console.error("id is undefined");
      return;
    }

    setIsRollingImage(true);
    //! API request to create the image
    axios
      .post(`/api/createDalle3/${id}`)
      .then((response) => {
        console.log("Response from createDalle3Image:", response.data.imageUrl);
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
        setIsRollingImage(false);
      });
  };

  return (
    <Button
      className="p-button-text p-button-plain menuItemButton border-bluegray-200 bg-bluegray-100"
      id={`spreadImageBtn-${id}`}
      data-cardid={id}
      onClick={createDalle3Image}
      icon="pi pi-fw pi-image"
      label='Roll Image'
      disabled={disabled}
    />
  );
};

RollImageButton.propTypes = {
  id: PropTypes.number.isRequired,
  setDalle3ImageUrlState: PropTypes.func.isRequired,
  setIsRollingImage: PropTypes.func.isRequired,
}

export default RollImageButton;
