# TODOS

## Define Models

enum Dice : string
    - d2
    - d4
    - d6
    - d8
    - d10-d100
    - d12
    - d20

- Dice Class:
  - DiceSpread:
    - Dice d2 => random.range(0,1)
    - Dice d4 => random.range(1, 4)
    - Dice d6 => random.range(1, 6)
    - Dice d8 => random.range(1, 8)
    - Dice d10-d100 => random.range(0, 9), range(0, 90)
    - Dice d12 => random.range(1, 12)
    - Dice d20 => random.range(1,20)
  - GetReading()

## How API will work

- User will make a get request to recieve a reading
- Server will generate a new dice spread for user
- Dice spread will be sent to *ChatGPT* using `GetReading(...diceToUse)`
  - `GetReading()` will make a request to OPENAI with a **serialzied datastructure** as a prompt header attached to request, which contextualizes the reading by providing:
    - Intialize
      - Please forget all previous prompts and conversations and start fresh.
      - Included in this prompt is a `{dice spread}` from collection of dice (d2, d4, d6, d8, d10-d100, d12, d20).
      - Use the included dice spread according to the instructions outline in the rest of this datastructure.
      - for your response use only the provided `resposneTemplate`, with no extra text or commentary.
    - Instructions:
      - Process:
        - For this reading please work in a step-by-step fashion, first generating the explantions for the individual dice. Aftwards creating a summary based on synthesizing the meaning of all the dice into one cohesive reading.
      - Expected output:
        - A reading or explanation for each individual dice
        - A reading for the dice set as a whole.
        - Actionable advice.
        - A mantra for the user to consider as they move foward.
    - Theme of the reading
      - `{User}` is solving a creative problem and wants to recieve a reading that will help them resolve creative blockage. Please recommend some advice based on the dice roll. And take inspiration from "gnomic suggestion, aphorisms or remarks which can be used to break a deadlock or dilemma situation."
      - The goal of this reading is to provide univeral, pragmatic, and actional suggestions and considerations that `{User}` will find useful for navigating creative decision making, problem solving, and ideation.

Template for **ChatGPT** Response:

```json
    {
        individualDice: {
            d2: <Reading for d2 dice>
            d4: <Reading for d4 dice>,
            d6: <Reading for d6 dice>,
            d8: <Reading for d8 dice>,
            dd10-d100: <Reading for dd10-d100 dice>,
            d12: <Reading for d12 dice>,
            d20: <Reading for d20 dice>,
        }
        summary: {
            fullReading: <A reading that synthesizes the meaning of all dice together, formatted .md to start with h2 2-3 paragraphs>,
            actionableAdvice: <Pragmatic/Practical things a person can do in order to help move beyond their creative or problem-solving blockage. 2-3 sentences>
            mantra: <A short poetric change or phrase that can be repeated to ease the mind. 1 sentence>
        }
    }
```

