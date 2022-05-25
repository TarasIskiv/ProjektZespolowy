import { jobTypes } from '../reducers/constants';

const setJob = (data) => {
    return (dispatch) => {
        dispatch({
            type: jobTypes.setJob,
            payload: {jobs: data}
        });
    }
}

const setActiveJob = (data) => {
    return (dispatch) => {
        dispatch({
            type: jobTypes.setJob,
            payload: {activeJob: data}
        });
    }
}

export {
    setJob,
    setActiveJob
}
