import Api from './index';

const newOffer = async (data) => {
    return await Api.post('/api/job/add', data);
}

const deleteOffer = async (id) => {
    return await Api.delete('/api/job/' + id);
}

const searchOffers = async (params) => {
    return await Api.get('/api/job/search?' + params);
}


const helper = {
    newOffer: newOffer,
    deleteOffer: deleteOffer,
    searchOffers: searchOffers
}

export default helper;