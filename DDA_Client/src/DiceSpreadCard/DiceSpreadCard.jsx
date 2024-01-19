import PropTypes from 'prop-types'

const DiceSpreadCard = ({ spread }) => {
    const { Id, lunarData, dalle3ImageUrl, interpretation } = spread

    const diceTypes = ['d2', 'd4', 'd6', 'd8', 'd10_100', 'd12', 'd20'];

    // ! Currentluy 'lunarData' and 'interpretations' are stringified objects stored in the DB
    let diceLunarData = JSON.parse(lunarData)
    let diceInterpretations = JSON.parse(interpretation)

    console.log("Dalle3ImageUrl", dalle3ImageUrl)

    return (
        <div className='diceSpreadCard' id={`spreadCard-id-${Id}`}>
            <div className='spreadCardHeader'>
                <h2 className='diceSpreadCardTime'>TIME</h2>
                <h3 className='diceSpreadCardID'>ID: {Id}</h3>
            </div>

            <div className='spreadCardOptions'>
                <button className='spreadImageBtn' id={`spreadImageBtn-${Id}`} data-cardid={Id} onClick={() => console.log("RollImage Not Working")}>Roll Image</button>
            </div>

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

            {dalle3ImageUrl && (
                <div className='dalle3Section'>
                    <img className='dalle3Img' src={dalle3ImageUrl} alt='Dalle3 Image' />
                </div>
            )}

            <div className='overviewSection'>
                <h3 className='overviewTitle'>Overview</h3>
                <p className='overviewText'>{diceInterpretations.overview_interpretation}</p>
            </div>

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