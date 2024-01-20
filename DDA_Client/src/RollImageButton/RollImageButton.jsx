import axios from "axios";
import { useState } from "react";

const RollImageButton = ({id, getAllDiceSpreads, setDiceSpreadContent}) => {
    const [isLoading, setIsLoading] = useState(false);

    const createDalle3Image = async () => {

        if (id === undefined) {
            console.error('id is undefined');
            return;
        }

        setIsLoading(true);
        axios.post(`/api/createDalle3/${id}`)
        .then((response) => {
            console.log("createDalle3", response.data);
            getAllDiceSpreads(setDiceSpreadContent)
        })
        .catch((error) => {
            console.error("Error createDalleUImage:", error);
        })
        .finally(() => {
            setIsLoading(false);
        });;
    }

    console.log("id", id)

    return (
        <button
            className='spreadImageBtn'
            id={`spreadImageBtn-${id}`} 
            data-cardid={id} 
            onClick={createDalle3Image}
            disabled={isLoading}
        >
            {isLoading ? "Rendering..." : "Roll Image"}
        </button>
    )
}

export default RollImageButton