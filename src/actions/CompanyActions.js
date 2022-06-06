import { companyTypes } from '../reducers/constants';

const setProfile = (data) => {
    console.log(data)
    return (dispatch) => {
        dispatch({
            type: companyTypes.setProfile,
            payload: data
        });
    }
}

export {
    setProfile
}
