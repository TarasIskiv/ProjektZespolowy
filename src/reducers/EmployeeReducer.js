import {employeeTypes} from './constants';

const INITIAL_STATE = {
    employees: [],
    activeEmployee: null
};

const reducer = (state = INITIAL_STATE, action) => {
    switch(action.type) {
        case employeeTypes.setEmployee:
            return { ...state,  ...action.payload };
        case employeeTypes.setActiveEmployee:
            return { ...state,  ...action.payload };
        default:
            return state;
    }
};

export default reducer;