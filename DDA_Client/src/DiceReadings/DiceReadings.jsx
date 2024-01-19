import { useState, useEffect } from 'react';
import axios from 'axios'
import DiceSpreadCard from '../DiceSpreadCard/DiceSpreadCard';

const DiceReadings = () => {
  const [diceSpreadContent, setDiceSpreadContent] = useState([]);

  useEffect(() => {
    axios.get('/api/getAllDiceSpreads')
      .then(response => {
        console.log(response)
        setDiceSpreadContent(response.data);
      })
      .catch(error => console.error('Error fetching dice rolls:', error));
  }, []);

  console.log("DiceSpreadContent:", diceSpreadContent)
  return (
      <div className='allSpreads'>
        {diceSpreadContent.map((spread) => {
          return <DiceSpreadCard key={spread.id} spread={spread} />
        })}
      </div>
  );
};

export default DiceReadings
