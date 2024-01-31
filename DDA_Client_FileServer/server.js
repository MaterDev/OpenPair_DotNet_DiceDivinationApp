const express = require('express');
const app = express();
const port = 3000; // You can choose any available port

// Serve static files from a specified directory, e.g., 'public'
app.use('/images', express.static('./storage/images/'));

// Specifically for rolled images
app.use('/rolledImages', express.static('./storage/images/diceSpreadRolledImages'));

app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
