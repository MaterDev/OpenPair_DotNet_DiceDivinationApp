import PropTypes from "prop-types";
import { formatTime } from "../../_utils";
import { Card } from "primereact/card";
import { Fieldset } from 'primereact/fieldset'; import { Image } from "primereact/image";
import { useState } from "react";
import CardOptions from "./CardOptions";
import ResultTable from "./ResultTable";

const DiceSpreadCard = ({ spread }) => {
    const { id, lunarData, dalle3ImageUrl, interpretation, date } = spread;
    const [dalle3ImageUrlState, setDalle3ImageUrlState] =
        useState(dalle3ImageUrl);
    const [isRollingImage, setIsRollingImage] = useState(false);

    const diceTypes = ["d2", "d4", "d6", "d8", "d10_100", "d12", "d20"];

    // ! Currentluy 'lunarData' and 'interpretations' are stringified objects stored in the DB
    let diceLunarData = JSON.parse(lunarData);
    let diceInterpretations = JSON.parse(interpretation);

    const cardStyle = {
        backgroundColor: "var(--surface-200)",
    };

    const graphicHeader = (
        <div style={{ position: "relative", overflow: "hidden", height: "300px" }}>
            {dalle3ImageUrlState ? (
                <Image
                    alt="Card"
                    src={`/fs/rolledImages/` + dalle3ImageUrlState}
                    style={{
                        position: "absolute",
                        top: "50%",
                        transform: "translateY(-50%)",
                    }}
                    preview
                />
            ) : (
                <div style={{ backgroundColor: 'var(--surface-400)', height: '100%', width: '100%' }}></div>
            )}
        </div>
    );

    return (
        <Card
            title={formatTime(date)}
            subTitle={`ID: ${id}`}
            style={cardStyle}
            header={graphicHeader}
        >
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
            <Fieldset className="overviewSection my-3" legend="Overview">
                {/* // ! Dice Interpretation Overview */}
                <p className="overviewText m-0">
                    {diceInterpretations.overview_interpretation}
                </p>
            </Fieldset>

            {/* // ! Spread Results - Section */}
            <Fieldset className="spreadResults my-3" legend="Spread Details" toggleable collapsed>
                <ResultTable
                    diceTypes={diceTypes}
                    spread={spread}
                    diceInterpretations={diceInterpretations}
                />
            </Fieldset>
        </Card>
    );
};

DiceSpreadCard.propTypes = {
    spread: PropTypes.object.isRequired,
};

export default DiceSpreadCard;
