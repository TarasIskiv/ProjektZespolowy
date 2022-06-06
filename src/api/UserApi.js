import Api from './index';
import axios from "axios";

const registerAccount = async (data) => {
    return await Api.post('/api/user/signup', data);
}

 const login = async (data) => {
    return await Api.post('/api/user/signin', data);
}

const getProfile = async () => {
    return await Api.get('/api/user/profile');
}

const getCV = async (data) => {
    // return await axios({
    //     url: 'https://pz-api-finfjob.azurewebsites.net/api/user/profile/cv/create',
    //     method: 'POST',
    //     body: data,
    //     withCredentials: true,
    // });
    return await Api.post('/api/user/profile/cv/create', data);
}

const helper = {
    registerAccount: registerAccount,
    login: login,
    getProfile: getProfile,
    getCV: getCV
}

export  { registerAccount, login };
export default helper;