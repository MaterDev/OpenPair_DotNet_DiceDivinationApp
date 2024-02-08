import { useEffect } from "react";
import DiceSpreadCard from "../DiceSpreadCard/DiceSpreadCard.jsx";
import { useSelector, useDispatch } from "react-redux";
import { getAllDiceSpreads, formatTime } from "../../_utils/";

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
            className='col-12 md:col-6 lg:col-4 xl:col-3'
            title={formatTime(spread.date)}
            subTitle={`ID: ${spread.id}`}
          >
            <DiceSpreadCard key={spread.id} spread={spread} />
          </div>
        );
      })}
    </div>
  )
}

export default DiceReadings
