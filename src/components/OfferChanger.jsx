import style from '../styles/components/offerChanger.module.scss';
import { AiOutlinePlus } from "react-icons/ai";
import React, { useEffect, useState } from 'react'
import SignForm from './SignForm';
import CompanyApi from "../api/CompanyApi";
import JobApi from '../api/JobApi';
import OfferRow from './OfferRow';
import companyReducer from "../reducers/CompanyReducer";
import {connect} from "react-redux";
import {useParams} from "react-router";

const OfferChanger = (props) => {
    const { id } = useParams();
    const [offerFields, setOfferFields] = useState(false);
    const [offers, setOffers] = useState([])


    useEffect(() => {
        console.log(props)
        const _id = props.isPublic ? id : props.profile.id
        CompanyApi.getProfileData(_id).then((res) => {
            //TODO: set reducer
            console.log(res.data.data)
            setOffers(res.data.data)
        })
    }, [])



    const onSend = (data) => {
        console.log(data);
        JobApi.newOffer(data)
          .then((result) => {
            console.log(result);
            //TODO: fix update state
              const _id = props.isPublic ? id : props.profile.id
              CompanyApi.getProfileData(_id).then((res) => {
                  setOffers(res.data.data)
              })

          })
          .catch((error) => {
          });

        //TODO: clear inputs after send
    }

    const onDelete = (id) => {
        JobApi.deleteOffer(id).then(() => {
            const id = props.profile.id
            CompanyApi.getProfileData(id).then((res) => {
                setOffers(res.data.data)
            })
        })
        console.log('deleted')
        //TODO: fix update state
    }

    const opened = offerFields ? style.opened : "";

    const fields = [
        { id: '1', name: 'Title', title: 'Title', type: 'text' },
        { id: '2', name: 'Description', title: 'Description', type: 'text' },
        { id: '3', name: 'Requirements', title: 'Requirements', type: 'text' },
        { id: '4', name: 'Responsibilities', title: 'Responsibilities', type: 'text' },
        { id: '5', name: 'Salary', title: 'Salary', type: 'text' },
    ]
    return (
        <div className={style.offerChanger} >
            <div className={[style.header, style.no_rows, opened].join(" ")}  >
                <h2>Job offers</h2>
                { !props.isPublic && <AiOutlinePlus size={50} onClick={() => setOfferFields(!offerFields)}/> }
            </div>
            <div className={!offerFields ? style.hidden : ""}>
                <SignForm
                    fields={fields}
                    onSend={onSend}
                    buttonTitle='Save' />
            </div>
            {
                offers.map((offer) => {
                    return <OfferRow data={offer} onDelete={onDelete} isPublic={props.isPublic} /> // TODO: fix isPublic
                })
            }
        </div>
    )
}

const mapStateToProps = (state) => {
    return {
        profile: state.companyReducer.profile
    }
}

export default connect(mapStateToProps, null)(OfferChanger)