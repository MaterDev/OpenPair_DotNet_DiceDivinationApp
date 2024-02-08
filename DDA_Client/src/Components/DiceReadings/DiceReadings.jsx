import { useEffect } from "react";
import DiceSpreadCard from "../DiceSpreadCard/DiceSpreadCard.jsx";
import { useSelector, useDispatch } from "react-redux";
import { getAllDiceSpreads } from "../../_utils/";
import Masonry, { ResponsiveMasonry } from "react-responsive-masonry"


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
    <ResponsiveMasonry
      columnsCountBreakPoints={{ 350: 1, 750: 2, 1500: 3 }}
      className="m-8"
    >
      <Masonry gutter="25px">
      {allSpreadCards?.map((spread) => {
        return (
          <div key={spread.id} >
            <DiceSpreadCard key={spread.id} spread={spread} />
          </div>
        );
      })}
      </Masonry>
    </ResponsiveMasonry>
  )
}

export default DiceReadings
