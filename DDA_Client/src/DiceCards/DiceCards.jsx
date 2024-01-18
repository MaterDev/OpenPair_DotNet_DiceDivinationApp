import { useState, useEffect } from 'react';
import axios from 'axios'

const DiceCards = () => {
  const [diceRollsContent, setDiceRollsContent] = useState('');

  useEffect(() => {
    axios.get('/api/getAllDiceRollsDOM')
      .then(response => {
        console.log(response)
        setDiceRollsContent(response.data);
      })
      .catch(error => console.error('Error fetching dice rolls:', error));
  }, []);

  return (
    <div id="allRolls" dangerouslySetInnerHTML={{ __html: diceRollsContent }}></div>
  );
};

export default DiceCards;
