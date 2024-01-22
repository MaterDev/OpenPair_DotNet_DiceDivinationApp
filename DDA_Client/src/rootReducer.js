import { combineReducers } from 'redux'

import allSpreadCardsReducer from './reducers/allSpreadCardsReducer'

const rootReducer = combineReducers({
    allSpreadCardsReducer: allSpreadCardsReducer,
})

export default rootReducer