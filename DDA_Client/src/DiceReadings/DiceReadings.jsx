import { useEffect } from 'react';
import DiceSpreadCard from '../DiceSpreadCard/DiceSpreadCard';
import { getAllDiceSpreads } from '../_utils';

const DiceReadings = ({diceSpreadContent, setDiceSpreadContent}) => {

  useEffect(() => {
    getAllDiceSpreads(setDiceSpreadContent);
  }, []);

  return (
      <div className='allSpreads'>
        {diceSpreadContent.map((spread) => {
          return (
              <DiceSpreadCard 
                key={spread.id} 
                spread={spread}
                setDiceSpreadContent={setDiceSpreadContent}
              />
            )
        })}
      </div>
  );
};

export default DiceReadings
