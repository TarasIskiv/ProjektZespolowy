import style from '../styles/components/profileInfo.module.scss';
import React, {useEffect, useRef, useState} from "react";
import {connect} from "react-redux";
import {setRole} from "../actions/MainActions";
import { AiOutlineEdit} from "react-icons/ai";
import UserApi from "../api/UserApi";
import CompanyApi from "../api/CompanyApi";
import Button from "../components/Button";
import {Upload} from "upload-js";


const EmployeePage = () => {
    const [isOpen, setIsOpen] = useState(false);

    useEffect(() => {
        const testData = UserApi.getProfile();

        testData.then(data => {
            console.log(data);
            const info = data.data.data;
            setEmail(info.email);
            setFirstName(info.firstName);
            setLastName(info.lastName);
            setPhone(info.contactNumber);
            setCity(info.city);
            setCountry(info.country);
            if(info.image !== null)
                setImage(info.image);
            else
                setImage('https://upcdn.io/kW15arzBi8UXboQLBDzy48e');
        })
    },[]);

    const onOpenClick = () => {
        setIsOpen(!isOpen);
    }

    const onBlur = (e) => {
        const data = { [e.target.name]: e.target.value }
        UserApi.updateProfile(data)
    }

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('*********');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phone, setPhone] = useState('');
    const [city, setCity] = useState('');
    const [country, setCountry] = useState('');
    const [image, setImage] = useState(null);
    const inputFile = useRef(null);


    const upload = new Upload({apiKey: "public_kW15arz5o6VQwfroqdzR5FnoKxiC"})
    const uploadFile = upload.createFileInputHandler({
        onUploaded: ({ fileUrl, fileId }) => {
            console.log(fileUrl)
            UserApi.updateProfile({ image: fileUrl });
            setImage(fileUrl);
            // alert(`File uploaded! ${fileUrl}`);
        }
    });


    return (
        <>
            <h1>Profile</h1>
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
                            <p>First name:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={firstName} 
                                    name={"firstName"} 
                                    type={'text'} 
                                    onChange={(e) => setFirstName(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Last name:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={lastName} 
                                    name={"lastName"} 
                                    type={'text'} 
                                    onChange={(e) => setLastName(e.target.value)}/>
                                <AiOutlineEdit size={16} />
                            </div>
                        </div>
                        <div className={style.field}>
                            <p>Phone:</p>
                            <div className={style.inputEdit}>
                                <input 
                                    onBlur={onBlur} 
                                    autoComplete={"qwe"} 
                                    value={phone}
                                    name={"contactNumber"}
                                    type={'text'} 
                                    onChange={(e) => setPhone(e.target.value)}/>
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
                        <p>City:</p>
                        <div className={style.inputEdit}>
                            <input
                                onBlur={onBlur}
                                autoComplete={"qwe"}
                                value={city}
                                name={"city"}
                                type={'text'}
                                onChange={(e) => setCity(e.target.value)}/>
                            <AiOutlineEdit size={16} />
                        </div>
                    </div>
                    </div>
                <div className={style.closeButton} onClick={onOpenClick}>Close</div>
            </div>
        </>
    );
}

const mapStateToProps = (state) => {
    return {
        role: state.mainReducer.role
    }
}

export default connect(mapStateToProps,null)(EmployeePage);