/**
 *  Formats a timestamp into a readable format - `MMMM Do, YYYY, h:mm a`
 * 
 *  *This particular format was chosen because without concatenating the date would be written with 'at' in the middle: January 13, 2024 at 9:22 PM*
 *   @param {number} timestamp - The timestamp to format
 *   @returns {string} - The formatted timestamp
 *  
 *  */ 

export function formatTime(timestamp) {
    let date = new Date(timestamp);

    // Format first to get the date and time, without time
    let formattedDate = date.toLocaleDateString("en-US", {
        year: "numeric",
        month: "long",
        day: "numeric"
    });

    // Then format again to get the time, without date
    let formattedTime = date.toLocaleTimeString("en-US", {
        hour: "numeric",
        minute: "numeric",
        hour12: true
    });

    // Return the formatted date and time.
    return `${formattedDate}, ${formattedTime}`;
}