import Header from "../components/Header";
import Background from "../components/Background";
import Footer from "../components/Footer";
import {useParams} from "react-router";
import styles from '../styles/pages/PublicEmployerProfilePage.module.scss';
import Button from "../components/Button";
import OfferChanger from "../components/OfferChanger";
import React, {useEffect, useState} from "react";
import CompanyApi from '../api/CompanyApi';

const PublicEmployerProfile = () => {
    const { id } = useParams();
    const [data, setData] = useState('');
    useEffect(() => {
        CompanyApi.getProfileById(id).then(res => {
            setData(res.data.data);
        })
    }, []);


    return (
        <>
            <Header />
            <div className="container">
                <div className={styles.main}>
                    <div className={styles.info}>
                        <img src='https://jf-staeulalia.pt/img/other/23/collection-apple-logo-outline.jpg' />
                        <div>
                            <h1>{data.companyName}</h1>
                            <Button text='View web site' />
                        </div>
                    </div>
                    <div className={styles.description}>
                        <p>{data.description}</p>
                    </div>
                    <div className={styles.offers}>
                        <OfferChanger isPublic={true} />
                    </div>
                </div>
            </div>
            <Background />
            <Footer />
        </>
    )
}

export default PublicEmployerProfile;