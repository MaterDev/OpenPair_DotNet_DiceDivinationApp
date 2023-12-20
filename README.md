# OpenPair: Dice Divination App

![Cover](images/cover.png)

## Project Description

The Dice Divination App is a web-based server that processes dice roll results submitted through a POST request. It then communicates with OpenAI's API to generate divination readings based on the roll. The server is built using .NET 5.0 and ASP.NET Core.

## Features

- **Dice Roll Interpretation**: Accepts roll results from a D20 dice set and interprets them.
- **OpenAI API Integration**: Uses OpenAI API for generating meaningful divination readings.
- **JSON Data Handling**: Structures and parses JSON data for efficient server-client communication.
- **Customizable Responses**: Tailors divination readings to different aspects of dice rolls.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- An active OpenAI API key
- ASP.NET Core runtime
- A tool for testing APIs like Postman (optional)

### Installation

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/your-username/OpenPair-DiceDivinationApp.git
   cd OpenPair-DiceDivinationApp
   ```

2. **Set Up OpenAI API Key**:
   - Store your OpenAI API key securely, preferably in user secrets or environment variables.

3. **Restore Dependencies**:
   - Run `dotnet restore` to install necessary NuGet packages.

### Running the Application

1. **Start the Server**:
   - Execute `dotnet run` within the project directory to start the server.

2. **Send a POST Request**:
   - Use an API testing tool or your client application to send a POST request with dice roll data to `/dice-divination`.

3. **Receive Divination Reading**:
   - The server will respond with a structured divination reading based on the dice roll.

## Testing

- Ensure the server correctly handles various input scenarios and validates dice roll data.
- Test the API using tools like Postman to confirm accurate responses.

## Deployment

- Deployment instructions for cloud platforms (Azure, AWS) can be added as per your deployment strategy.

## Contributing

Contributions to enhance the Dice Divination App are welcome. Please follow standard coding practices and add tests for new features.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MDGUL License. See `LICENSE` file for more information.
