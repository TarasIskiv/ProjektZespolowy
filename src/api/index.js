import axios from "axios";
import Cookies from "universal-cookie";


const Api = axios.create({
    //baseURL: 'https://localhost:7253',
    baseURL: 'https://pz-api-finfjob.azurewebsites.net/',
    timeout: 5000,
    withCredentials: true,
});

Api.interceptors.request.use(function (config) {
    const cookies = new Cookies();
    const token = cookies.get('jwt');

    config.headers.Authorization =  token ? `Bearer ${token}` : '';
    return config;
});

Api.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

export default Api;