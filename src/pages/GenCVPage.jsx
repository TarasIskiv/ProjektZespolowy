import Header from "../components/Header";
import Background from "../components/Background";
import Footer from "../components/Footer";
import style from '../styles/pages/genCVPage.module.scss';
import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { setRole } from "../actions/MainActions";
import UserApi from "../api/UserApi";
import Button from '../components/Button'
import { Link } from "react-router-dom";

const GenCVPage = () => {

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
            if (info.userAddress != null)
                setLocation(info.userAddress);

        })
    }, []);

    const onOpenClick = () => {
        setIsOpen(!isOpen);
    }

    const sendInfo = () => {
        let data =
        {
            'email': email,
            'firstName': firstName,
            'lastName': lastName,
            'phone': phone,
            'location': location,
            'bio': bio,
            'template': template
        };

        UserApi.getCV(data).then((response) => {
            console.log(response.data);
            const url = window.URL.createObjectURL(new Blob([response.data], {
                type: 'application/pdf'
            }));
            console.log(url);
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'CV.pdf');
            document.body.appendChild(link);
            link.click();
            link.remove();
        }).catch((error) => {
            console.log(error);
        });

    }

    const onBlur = (e) => {
        console.log(e.target.value);
    }

    const [email, setEmail] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phone, setPhone] = useState('');
    const [location, setLocation] = useState('');
    const [bio, setBio] = useState('');

    const [template, setTemplate] = useState(1);

    return (
        <>
            <Header />
            <div className="container">
                <div className="main-block">
                    <div className="profile">
                        <h1>CV creator</h1>
                        <div className={style.columns} >
                            <div className={style.preview} >
                                <select
                                    value={template}
                                    onChange={(e) => setTemplate(Number(e.target.value))}
                                >
                                    <option value="1">Template 1</option>
                                    <option value="2">Template 2</option>
                                    <option value="3">Template 3</option>
                                </select>
                            </div>
                            <div className={`${style.fieldsList}`}>
                                <div className={style.field}>
                                    <p>E-mail:</p>
                                    <div className={style.inputEdit}>
                                        <input
                                            onBlur={onBlur}
                                            autoComplete={"qwe"}
                                            value={email}
                                            name={"email"}
                                            type={"text"}
                                            onChange={(e) => setEmail(e.target.value)} />
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
                                            onChange={(e) => setFirstName(e.target.value)} />
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
                                            onChange={(e) => setLastName(e.target.value)} />
                                    </div>
                                </div>
                                <div className={style.field}>
                                    <p>Phone:</p>
                                    <div className={style.inputEdit}>
                                        <input
                                            onBlur={onBlur}
                                            autoComplete={"qwe"}
                                            value={phone}
                                            name={"phone"}
                                            type={'text'}
                                            onChange={(e) => setPhone(e.target.value)} />
                                    </div>
                                </div>
                                <div className={style.field}>
                                    <p>Location:</p>
                                    <div className={style.inputEdit}>
                                        <input
                                            onBlur={onBlur}
                                            autoComplete={"qwe"}
                                            value={location}
                                            name={"location"}
                                            type={'text'}
                                            onChange={(e) => setLocation(e.target.value)} />
                                    </div>
                                </div>
                                <div className={style.field}>
                                    <p>Bio:</p>
                                    <div className={style.inputEdit}>
                                        <textarea
                                            onBlur={onBlur}
                                            autoComplete={"qwe"}
                                            value={bio}
                                            name={"bio"}
                                            type={'text'}
                                            onChange={(e) => setBio(e.target.value)} />
                                    </div>
                                </div>
                                <Button onClick={sendInfo} text="Create CV" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <Background />
            <Footer />
        </>
    );
}
const mapStateToProps = (state) => {
    return {
        role: state.mainReducer.role
    }
}

export default connect(mapStateToProps, null)(GenCVPage);