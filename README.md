# OpenPair: Dice Divination App

![Cover](images/cover.png)

## Project Description

> The Dice Divination App is a web-based server application that processes dice roll results and generates divination readings using OpenAI's ChatGPT. Built with .NET 8.0 and ASP.NET Core, the app leverages numerology and gamatria principles to interpret dice rolls from a D20 dice set.

## Features

- **Dice Roll Processing**: Accepts and processes dice roll results from a D20 dice set.
- **OpenAI ChatGPT Integration**: Communicates with OpenAI's ChatGPT to provide numerological and gamatria interpretations of dice rolls.
- **Divination Interpretation**: Generates insightful readings based on the unique combination of dice rolls.
- **JSON Data Handling**: Efficiently structures and parses JSON data for server-client communication.

## Components

- **Models**: Contains the `Dice.cs` model defining the structure of a dice roll.
- **Controllers**: Includes `DiceSpread.cs` and `ChatGPT.cs` controllers for handling dice roll processing and ChatGPT communication.
- **Entities**: Houses the `DiceSpread.cs` entity representing the dice roll data.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- An active OpenAI API key
- ASP.NET Core runtime
- API testing tool like Postman (optional)

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/MaterDev/OpenPair_DotNet_DiceDivinationApp
   cd OpenPair_DotNet_DiceDivinationApp/DDA_Server
   ```

2. **Set Up OpenAI API Key**:
   - Add your OpenAI API key to the `.env` file.

3. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

### Database Setup

To set up the database for the Dice Divination App:

1. **Configure the Database Connection**:
   - In the `appsettings.json`, update the connection string under `ConnectionStrings:DefaultConnection` with your database details.

2. **Run Migrations**:
   - In the terminal, navigate to the project directory and run:
     ```bash
     dotnet ef database update
     ```
   - This will apply the migrations and set up your database schema.

### Running the Application

1. **Start the Server**:
   ```bash
   dotnet run
   ```

2. **Test the API**:
   - Use Postman to send dice roll data to the server and receive divination readings.

## API Usage

The Dice Divination App offers several API endpoints for dice roll processing and divination interpretation:

1. **Submit Dice Rolls**:
   - **Endpoint**: `/getSpread`
   - **Method**: `GET`
   - **Description**: This endpoint responds with a spread of dice roll results, including a date. This spread is also stored in the database upon creation.
   - **Example Response**:
     ```json
     {
         "d2": 1,
         "d4": 4,
         "d6": 2,
         "d8": 4,
         "d10_100": 10,
         "d12": 7,
         "d20": 17,
         "date": "2024-01-04T21:47:29.35563Z"
      }
     ```

2. **Get Interpretation**:
   - **Endpoint**: `/interpretDice/{id}`
   - **Method**: `GET`
   - **Description**: Retrieves a stored divination interpretation by its unique ID and uses ChatGPT to produce a set of interpretations using "JSON mode".
   - **Example Response**: 
      ```json
      {
         "overviewInterpretation": "The collective significance of these dice rolls bridges the gap between chance and destiny, weaving a tapestry of numerical symbolism that reflects the intricate balance of dualities, growth, challenges, and potential in life's journey. Each number carries with it a vibration that resonates with ancient wisdom and esoteric knowledge, offering insights into the universal energies at play.",
         "diceInterpretations": {
            "d2": "The D2 roll represents duality, balance, and the binary nature of reality. In numerology, the number 2 is associated with partnership and harmony, while in gamatria, it can symbolize the principle of creation and division. This roll may indicate a need for cooperation and the reconciliation of opposites.",
            "d4": "The D4 roll embodies stability, order, and the grounding energy of the material world. In numerology, the number 4 is linked to practicality and building solid foundations. In gamatria, it can represent the tetragrammaton, the four-letter name of God, suggesting a connection to divine will and the structure of existence.",
            "d6": "The D6 roll signifies balance, harmony, and the nurturing aspects of life. Numerologically, 6 is associated with responsibility and service to others, while in gamatria, it may represent the six days of creation, indicating productivity and the beauty of the natural world.",
            "d8": "The D8 roll is a symbol of abundance, power, and material success. In numerology, 8 is the number of karma and manifests rewards for hard work. In gamatria, it can signify the superabundance and the transcendence of natural order, hinting at a connection to spiritual realms.",
            "d10_100": "The D10_100 roll, depending on whether it is read as 10 or 100, carries the essence of completion and infinite potential. The number 10 in numerology represents wholeness and the return to unity, while 100 amplifies these energies. In gamatria, these numbers can signify the perfection of divine order and the fullness of creation.",
            "d12": "The D12 roll reflects cosmic order and the completion of cycles. Numerologically, 12 is often associated with the completion of a cycle and the experience of cosmic consciousness. In gamatria, it can symbolize the twelve tribes of Israel or the zodiac, representing the diversity and unity of life's experiences.",
            "d20": "The D20 roll is indicative of potential for transformation and the testing of one's limits. In numerology, the number 20 can be seen as a call to trust in the divine and to embrace one's path with faith. In gamatria, it may represent the power of resurrection and renewal, suggesting a period of significant change and rebirth."
         }
      }
      ```

## License

Distributed under the MDGUL License. See `LICENSE` file for more information.
