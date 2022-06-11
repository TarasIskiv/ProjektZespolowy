import { jobTypes } from './constants';

const INITIAL_STATE = {
    jobs: [],
    activeJob: null
};

const reducer = (state = INITIAL_STATE, action) => {
    switch(action.type) {
        case jobTypes.setJob:
            return { ...state,  ...action.payload };
        case jobTypes.setActiveJob:
            return { ...state,  ...action.payload };
        default:
            return state;
    }
};

export default reducer;


