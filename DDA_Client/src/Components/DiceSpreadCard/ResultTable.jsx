import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';


const ResultTable = ({diceInterpretations, diceTypes, spread}) => {

    const data = diceTypes.map((dice) => ({
        dice: dice.toUpperCase(),
        result: spread[dice],
        interpretation: diceInterpretations.dice_interpretations[dice],
      }));

  return (
    <DataTable size='small' value={data} className="spreadResultTable my-3">
      <Column field="dice" header="Dice"></Column>
      <Column field="result" header="Result"></Column>
      <Column field="interpretation" header="Interpretation"></Column>
    </DataTable>
  );
};

export default ResultTable;
