import Api from './index';

const searchEmployees = async (params) => {
    return await Api.get('/api/user/search?' + params);
}


const helper = {
    searchEmployees: searchEmployees
}

export default helper;