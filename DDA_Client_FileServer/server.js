const express = require('express');
const app = express();
const port = 3000; 

// Serve static files from a specified directory, e.g., 'public'
app.use('/fs/images', express.static('./storage/images/'));

// Specifically for rolled images
app.use('/fs/rolledImages', express.static('./storage/images/diceSpreadRolledImages'));

app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
