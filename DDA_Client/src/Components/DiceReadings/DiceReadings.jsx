import { useEffect } from "react";
import DiceSpreadCard from "../DiceSpreadCard/DiceSpreadCard.jsx";
import { useSelector, useDispatch } from "react-redux";
import { getAllDiceSpreads } from "../../_utils/";

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
  }, [dispatch])

  return (
    <div  id="diceReadings" className="grid">
      {allSpreadCards?.map((spread) => {
        return (
          <div
            key={spread.id}
            className='col-12 md:col-6 xl:col-4'
            
          >
            <DiceSpreadCard key={spread.id} spread={spread} />
          </div>
        );
      })}
    </div>
  )
}

export default DiceReadings
