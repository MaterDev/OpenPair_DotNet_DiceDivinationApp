import { useState } from "react";
import axios from "axios";
import DiceReadings from "./DiceReadings/DiceReadings";

function App() {
  const [diceSpreadContent, setDiceSpreadContent] = useState([]);

  const createSpread = (event) => {
    // Prevent form submission from refreshing the page
    event.preventDefault();

    // Display a loader
    document.getElementById("pleaseWait").innerHTML +=
      '<div id="loader">Creating... (Please Meditate)</div>';

    // Loading audio for while the request is being made
    var loadingAudio = new Audio("/assets/uplifting-pad-texture-113842.mp3");
    loadingAudio.play();

    // Done audio for when the request is complete
    var doneAudio = new Audio("/assets/hotel-bell-ding-1-174457.mp3");

    fetch("/api/createSpread", { method: "POST" })
      .then(() => {
        // Once the request is complete, remove the loader, stop the audio, and refresh the page
        document.getElementById("loader").remove();
        loadingAudio.pause();
        loadingAudio.currentTime = 0;
        doneAudio.play();
        // Reload the page after the done audio has finished
        doneAudio.onended = function () {
          console.log("done");
          location.reload();
        };
      })
      .catch((error) => {
        // If there's an error, remove the loader, stop the audio, and display the error
        document.getElementById("loader").remove();
        loadingAudio.pause();
        loadingAudio.currentTime = 0;
        console.error("Error:", error);
      });
  };

  return (
    <>
      <header id="pageHeader">
        <span id="title">The Path</span>
        <span id="pleaseWait"></span>
        <button
          id="createSpreadBtn"
          type="submit"
          onClick={() => createSpread(event)}
        >
          🎲
        </button>
      </header>

      <DiceReadings setDiceSpreadContent={setDiceSpreadContent} diceSpreadContent={diceSpreadContent}/>
    </>
  );
}

export default App;
