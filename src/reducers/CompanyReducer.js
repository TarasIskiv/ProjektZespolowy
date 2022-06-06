import { companyTypes } from "./constants";

const INITIAL_STATE = {
    profile: {
        id: null,
        companyName: '',
        companyAddress: '',
        email: '',
        website: '',
        description: ''
    },
    jobs: {}
};

const reducer = (state = INITIAL_STATE, action) => {
    switch(action.type) {
        case companyTypes.setProfile:
            return { ...state,  profile: action.payload };
        case companyTypes.setJobs:
            return { ...state,  ...action.payload };
        case companyTypes.clear:
            return { INITIAL_STATE };
        default:
            return state;
    }
};

export default reducer;