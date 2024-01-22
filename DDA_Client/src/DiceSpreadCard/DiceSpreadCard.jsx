import { useState } from 'react';
import PropTypes from 'prop-types'
import { formatTime, getAllDiceSpreads } from '../_utils/';
import RollImageButton from '../RollImageButton/RollImageButton';

const DiceSpreadCard = ({ spread }) => {

    console.log('DiceSpreadCard.jsx - spread:', spread)
    const { id, lunarData, dalle3ImageUrl, interpretation } = spread

    // State for dall3 image
    const [dalle3ImageUrlState, setDalle3ImageUrlState] = useState(dalle3ImageUrl);

    const diceTypes = ['d2', 'd4', 'd6', 'd8', 'd10_100', 'd12', 'd20'];

    // ! Currentluy 'lunarData' and 'interpretations' are stringified objects stored in the DB
    let diceLunarData = JSON.parse(lunarData)
    let diceInterpretations = JSON.parse(interpretation)

    return (
        <div className='diceSpreadCard' id={`spreadCard-id-${id}`}>

            {/* // ! Header - Section */}
            <div className='spreadCardHeader'>
                <h2 className='diceSpreadCardTime'>{formatTime(spread.date)}</h2>
                <h3 className='diceSpreadCardID'>ID: {id}</h3>
            </div>

            {/* // ! Options - Section */}
            <div className='spreadCardOptions'>
                <RollImageButton
                    id={id}
                />
            </div>

            {/* // ! Lunar Data - Section */}
            {lunarData && (
                <>
                    <hr />
                    <div className='lunarDataSection'>
                        <span className='lundarPhaseTxt'>{diceLunarData.phase}</span>
                        <span className='lundarPhaseEmoji'>{diceLunarData.moon_phase_emoji} - </span>
                        <span className='lundarZodiacTxt'>{diceLunarData.zodiac}</span>
                        <span className='lundarZodiacEmoji'>{diceLunarData.zodiac_emoji}</span>
                    </div>
                    <hr />
                </>
            )}

            {/* // ! Dalle3 Image - Section */}
            {dalle3ImageUrl && (
                <div className='dalle3Section'>
                    <img className='dalle3Img' src={dalle3ImageUrlState} alt='Dalle3 Image' />
                </div>
            )}
            
            {/* // ! Overview - Section */}
            <div className='overviewSection'>
                <h3 className='overviewTitle'>Overview</h3>
                <p className='overviewText'>{diceInterpretations.overview_interpretation}</p>
            </div>
            
            {/* // ! Spread Results - Section */}
            <table className='spreadResultTable'>
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
        </div>
    );
}

DiceSpreadCard.propTypes = {
    spread: PropTypes.object.isRequired
}

export default DiceSpreadCard