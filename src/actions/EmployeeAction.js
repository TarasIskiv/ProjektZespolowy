import { employeeTypes } from '../reducers/constants';

const setEmployee = (data) => {
    return (dispatch) => {
        dispatch({
            type: employeeTypes.setEmployee,
            payload: {employees: data}
        });
    }
}

const setActiveEmployee = (data) => {
    return (dispatch) => {
        dispatch({
            //type: employeeTypes.setActiveEmployee,
            type: employeeTypes.setEmployee,
            payload: {activeEmployee: data}
        });
    }
}

export {
    setEmployee,
    setActiveEmployee
}
