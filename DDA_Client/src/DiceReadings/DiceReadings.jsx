import { useEffect } from "react";
import DiceSpreadCard from "../DiceSpreadCard/DiceSpreadCard";
import { useSelector, useDispatch } from "react-redux";
import { getAllDiceSpreads } from "../_utils/get.requests.js";

const DiceReadings = () => {
  const allSpreadCards = useSelector((state) => state.allSpreadCardsReducer);
  const dispatch = useDispatch();

  useEffect(() => {
    getAllDiceSpreads()
    .then((data) => {
      dispatch({ type: "LOAD_STATE", payload: data })
    })
    .catch((error) =>
        console.error("Error DiceReadings(), getAllDiceSpreads:", error)
      )
  })

  return (
    <div className="allSpreads">
      {allSpreadCards?.map((spread) => {
        return <DiceSpreadCard key={spread.id} spread={spread} />;
      })}
    </div>
  )
}

export default DiceReadings
