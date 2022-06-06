import Api from './index';

const newOffer = async (data) => {
    return await Api.post('/api/job/add', data);
}

const deleteOffer = async (id) => {
    return await Api.delete('/api/job/' + id);
}

const helper = {
    newOffer: newOffer,
    deleteOffer: deleteOffer
}

export default helper;