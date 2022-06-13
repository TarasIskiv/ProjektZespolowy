import OfferChanger from "../components/OfferChanger";
import style from '../styles/components/profileInfo.module.scss';
import React, {useEffect, useRef, useState} from "react";
import {connect} from "react-redux";
import { setProfile } from "../actions/CompanyActions";
import { AiOutlineEdit } from "react-icons/ai";
import CompanyApi from "../api/CompanyApi";
import {Upload} from "upload-js";
import UserApi from "../api/UserApi";


const EmployerPage = (props) => {

    const [isOpen, setIsOpen] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('*********');
    const [name, setName] = useState('');
    const [country, setCountry] = useState('');
    const [city, setCity] = useState('');
    const [website, setWebsite] = useState('');
    const [description, setDescription] = useState('');
    const [image, setImage] = useState(null);
    const inputFile = useRef(null);


    const upload = new Upload({apiKey: "public_kW15arz5o6VQwfroqdzR5FnoKxiC"})
    const uploadFile = upload.createFileInputHandler({
        onUploaded: ({ fileUrl, fileId }) => {
            console.log(fileUrl)
            CompanyApi.updateProfile({ image: fileUrl });
            setImage(fileUrl);
            // alert(`File uploaded! ${fileUrl}`);
        }
    });

    useEffect(() => {
        const testData = CompanyApi.getProfile();

        testData.then(data => {
            console.log(data);
            const info = data.data.data;
            const getOffers = CompanyApi.getProfileData(info.id);

            props.setProfile(info)
            console.log(info);
            setEmail(info.email);
            setName(info.companyName);
            setWebsite(info.website);
            setDescription(info.description);
            setCity(info.city);
            setCountry(info.country);

            if(info.image !== null)
                setImage(info.image);
            else
                setImage('https://upcdn.io/kW15arzBi8UXboQLBDzy48e');
        });
    },[]);

    const onOpenClick = () => {
        setIsOpen(!isOpen);
    }

    const onBlur = (e) => {
        const data = { [e.target.name]: e.target.value }
        CompanyApi.updateProfile(data)
    }

    return (
        <>
            <h1>Company profile</h1>
            <img src={image} onClick={() => {
                console.log('click')
                inputFile.current.click()
            }}/>
            <input type='file' ref={inputFile} style={{display: 'none'}} onChange={uploadFile}/>
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
                            <p>Country:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={country}
                                    name={"country"}
                                    type={'text'} 
                                    onChange={(e) => setCountry(e.target.value)}/>
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
                            <p>City:</p>
                            <div className={style.inputEdit}>
                                <input
                                    onBlur={onBlur}
                                    autoComplete={"qwe"}
                                    value={city}
                                    name={"City"}
                                    type={'text'}
                                    onChange={(e) => setCity(e.target.value)}/>
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