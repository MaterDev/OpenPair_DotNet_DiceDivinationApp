import PropTypes from "prop-types";
import { formatTime } from "../../_utils";
import { Card } from "primereact/card";
import { Panel } from "primereact/panel";
import { Image } from "primereact/image";

import { useState } from "react";
import CardOptions from "./CardOptions";

const DiceSpreadCard = ({ spread }) => {
  const { id, lunarData, dalle3ImageUrl, interpretation, date } = spread;
  const [dalle3ImageUrlState, setDalle3ImageUrlState] =
    useState(dalle3ImageUrl);
  const [imageLoading, setImageLoading] = useState(true);
  const [isRollingImage, setIsRollingImage] = useState(false);

  const handleImageLoad = () => {
    setImageLoading(false);
  };

  const diceTypes = ["d2", "d4", "d6", "d8", "d10_100", "d12", "d20"];

  // ! Currentluy 'lunarData' and 'interpretations' are stringified objects stored in the DB
  let diceLunarData = JSON.parse(lunarData);
  let diceInterpretations = JSON.parse(interpretation);

  const cardStyle = {
    backgroundColor: "var(--surface-200)",
  };

  return (
    <Card title={formatTime(date)} subTitle={`ID: ${id}`} style={cardStyle}>
      {/* // ! Options - Section */}
      <CardOptions
        isRollingImage={isRollingImage}
        spreadId={id}
        setDalle3ImageUrlState={setDalle3ImageUrlState}
        setIsRollingImage={setIsRollingImage}
      />

      {/* // ! Lunar Data - Section */}
      {lunarData && (
        <div className="lundarData my-3">
          <hr />
          <span className="lundarPhaseTxt">{diceLunarData.phase}</span>
          <span className="lundarPhaseEmoji">
            {" "}
            {diceLunarData.moon_phase_emoji} -{" "}
          </span>
          <span className="lundarZodiacTxt">{diceLunarData.zodiac}</span>
          <span className="lundarZodiacEmoji">
            {" "}
            {diceLunarData.zodiac_emoji}
          </span>
          <hr />
        </div>
      )}

      {/* // ! Overview - Section */}
      <Panel className="overviewSection my-3" header="Overview">
        <p className="overviewText m-0">
          {diceInterpretations.overview_interpretation}
        </p>
      </Panel>

      {/* // ! Dalle3 Image - Section */}
      <Image
        key={dalle3ImageUrlState}
        imageClassName="dalle3Section my-3 w-full"
        src={`/fs/rolledImages/` + dalle3ImageUrlState}
        alt="Dalle3 Image"
        onLoad={handleImageLoad}
        style={imageLoading ? { display: "none" } : {}}
        preview
      />



      {/* // ! Spread Results - Section */}
      <table className="spreadResultTable my-3">
        <thead>
          <tr>
            <th>Dice</th>
            <th>Result</th>
            <th>Interpretation</th>
          </tr>
        </thead>
        <tbody>
          {diceTypes.map((dice) => (
            <tr key={dice}>
              <th>{dice.toUpperCase()}</th>
              <td>{spread[dice]}</td>
              <td>{diceInterpretations.dice_interpretations[dice]}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </Card>
  );
};

DiceSpreadCard.propTypes = {
  spread: PropTypes.object.isRequired,
};

export default DiceSpreadCard;
