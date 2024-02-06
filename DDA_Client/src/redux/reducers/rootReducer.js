import { combineReducers } from 'redux'

import allSpreadCardsReducer from './allSpreadCardsReducer'

const rootReducer = combineReducers({
    allSpreadCardsReducer: allSpreadCardsReducer,
})

export default rootReducer