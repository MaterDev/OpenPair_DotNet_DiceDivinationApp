const allSpreadCardsReducer = (state = [], action) => {
  switch (action.type) {
    case "LOAD_STATE": {
      if (typeof action.payload === "undefined") {
        // If it is, return the current state
        return state;
      }
      return [...action.payload];
    }
    default:
      return state;
  }
};

export default allSpreadCardsReducer;
