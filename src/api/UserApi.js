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
    return await Api.post('/api/user/cv', data, { responseType: 'blob'});
}

const test = async (data) => {
    return await Api.post('/api/user/test/' + data);
}

const helper = {
    registerAccount: registerAccount,
    login: login,
    getProfile: getProfile,
    getCV: getCV,
    test: test
}

export  { registerAccount, login };
export default helper;