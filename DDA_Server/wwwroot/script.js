
console.log("Script loaded");

function createSpread(event) {
    // Prevent form submission from refreshing the page
    event.preventDefault();

    // Display a loader
    document.getElementById("pleaseWait").innerHTML +=
        '<div id="loader">Creating... (Please Meditate)</div>';

    // Loading audio for while the request is being made
    var loadingAudio = new Audio(
        "/assets/uplifting-pad-texture-113842.mp3"
    );
    loadingAudio.play();

    // Done audio for when the request is complete
    var doneAudio = new Audio("/assets/hotel-bell-ding-1-174457.mp3");

    fetch("/createSpread", { method: "POST" })
        .then((response) => {
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
}

function rollImage(event) {

    console.log('Rolling Image');

    let allImageRollBtns = document.querySelectorAll('.rollImageBtn');
    console.log('All Image Roll Buttons: ' + allImageRollBtns);
    console.log(allImageRollBtns);
    allImageRollBtns.forEach(element => {
        console.log(element);
        element.disabled = true;
    });

    console.log('Element ID: ' + event.target);
    let id = event.target.id;
    let cardID = event.target.getAttribute('data-cardid')
    
    fetch("/createDalle3/" + cardID, { method: "POST" })
        .then((response) => {
            console.log("Success:", response);
            // enable all rollImage buttons after fetch is done.
            allImageRollBtns.forEach(element => {
                console.log(element);
                element.disabled = false;
            });
            location.reload();
        })
        .catch((error) => {
            console.error("Error:", error);
            // enable all rollImage buttons after fetch is done.
            allImageRollBtns.forEach(element => {
                console.log(element);
                element.disabled = false;
            });
            document.getElementById(id).disabled = false;
        });
}
