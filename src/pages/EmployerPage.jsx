import OfferChanger from "../components/OfferChanger";
import style from '../styles/components/profileInfo.module.scss';
import React, {useEffect, useState} from "react";
import {connect} from "react-redux";
import { setProfile } from "../actions/CompanyActions";
import { AiOutlineEdit } from "react-icons/ai";
import CompanyApi from "../api/CompanyApi";


const EmployerPage = (props) => {

    const [isOpen, setIsOpen] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('*********');
    const [name, setName] = useState('');
    const [location, setLocation] = useState('');
    const [website, setWebsite] = useState('');
    const [description, setDescription] = useState('');

    useEffect(() => {
        const testData = CompanyApi.getProfile();

        testData.then(data => {
            console.log(data);
            const info = data.data.data;
            const getOffers = CompanyApi.getProfileData(info.id);

            props.setProfile(info)
            console.log(info)
            setEmail(info.email);
            setName(info.companyName);
            setWebsite(info.website);
            setDescription(info.description);
            if(info.companyAddress != null)
                setLocation(info.companyAddress);
        });


    },[]);

    const onOpenClick = () => {
        setIsOpen(!isOpen);
    }

    const onBlur = (e) => {
        const data = { [e.target.name]: e.target.value }
        CompanyApi.updateProfile(data)

        console.log(e.target.value);
    }


    return (
        <>
            <h1>Company profile</h1>
            <img src="https://thispersondoesnotexist.com/image" />
            <div className={`${style.profileInfo} ${isOpen ? style.isOpen : ''}`}>
                <div className={`${style.fieldsList} ${isOpen ? style.hideItems : ''}`}>
                        <div className={style.field}>
                            <p>E-mail:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={email} 
                                    name={"email"} 
                                    type={"text"} 
                                    onChange={(e) => setEmail(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Password:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={password} 
                                    name={"password"} 
                                    type={'password'} 
                                    onChange={(e) => setPassword(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Company name:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={name} 
                                    name={"companyName"}
                                    type={'text'} 
                                    onChange={(e) => setName(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Location:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={location} 
                                    name={"companyAddress"}
                                    type={'text'} 
                                    onChange={(e) => setLocation(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Website:</p>
                            <div className={style.inputEdit}>
                                <input
                                    onBlur={onBlur}
                                    autoComplete={"qwe"}
                                    value={website}
                                    name={"website"}
                                    type={'text'}
                                    onChange={(e) => setWebsite(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Edit:</p>
                            <div className={style.inputEdit}>
                                <input
                                    onBlur={onBlur}
                                    autoComplete={"qwe"}
                                    value={website}
                                    name={"Edit"}
                                    type={'text'}
                                    onChange={(e) => setWebsite(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field} style={{ flex: 1}}>
                            <p>Description:</p>
                            <div className={style.inputEdit}>
                                    <textarea
                                        onBlur={onBlur}
                                        autoComplete={"qwe"}
                                        value={description}
                                        name={"description"}
                                        type={'text'}
                                        onChange={(e) => setDescription(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                    </div>
                <div className={style.closeButton} onClick={onOpenClick}>Close</div>
            </div>
            <OfferChanger isPublic={false} />
        </>
    );
}
const mapStateToProps = (state) => {
    return {
        role: state.mainReducer.role,
        profile: state.companyReducer.profile
    }
}

export default connect(mapStateToProps,{ setProfile })(EmployerPage);