import Header from "../components/Header";
import Footer from "../components/Footer";
import Background from "../components/Background";
import SignForm from "../components/SignForm";
import {useEffect, useState} from "react";

import { setError, setErrorText } from '../actions/MainActions';
import { connect } from 'react-redux';
import UserApi from "../api/UserApi";
import CompanyApi from "../api/CompanyApi";
import style from "../styles/pages/signPage.scss"


const SignUpPage = (props) => {
    const sameFields = [
        { id: '3', name:'email', title: 'Email', type: 'text' },
        { id: '4', name:'password', title: 'Password', type: 'password' },
        { id: '5', name:'repeatPassword', title: 'Repeat password', type: 'password' },
    ];

    const fieldsEmployee = [
        { id: '1', name:'firstName', title: 'First Name', type: 'text' },
        { id: '2', name:'lastName', title: 'Last Name', type: 'text' },
    ].concat(sameFields);

    const fieldsEmployer = [
        { id: '1', name:'companyName', title: 'Employer Name', type: 'text' }
    ].concat(sameFields);

    const [fields, setFields] = useState(fieldsEmployee);

    const onSend = (data) => {
        for(const key in data) {
            if(data[key] === '') {
                props.setError(true);
                props.setErrorText(key + " cannot be empty");
                return;
            }
        }

        if(data['password'] != data['repeatPassword'])
        {
            props.setError(true);
            props.setErrorText("Passwords do not match");
            return;
        }

        delete data['repeatPassword'];
        const send = props.tab ?  CompanyApi.registerAccount : UserApi.registerAccount;
        // 0 - company
        // 1 - user     
        send(data)
            .then((result) => {
                if(result.data === "OK"){
                    props.setError(false);
                }
                else {
                    props.setError(true);
                    props.setErrorText(result.data);
                }
            })
            .catch((error) => {

                if(error.response !== undefined) {
                    props.setError(true);
                    if('data' in error.response.data)
                        props.setErrorText(error.response.data.data);
                    else
                        props.setErrorText(Object.values(error.response.data.errors)[0][0]);
                } else {
                    props.setError(true);
                    props.setErrorText("undefined response!");
                }
            });
    }

    const setTitle = (tab) => {
        const type = (tab ? 'Employer' : 'Employee') ;
        return type + ' register';
    }

    const whichFields = (tab) => {
        return (tab? fieldsEmployer : fieldsEmployee);
    }

    useEffect(() => {
        setFields(whichFields(props.tab));

        props.setError(false);

        return () => {
            console.log("unmount");
            props.setError(false);
        }
    },[props.tab]);

    return (
        <>
            <Header />
            <div className="container">
                <SignForm
                    title={setTitle(props.tab)}
                    fields={fields}
                    onSend={onSend}
                    buttonTitle='Register' />
            </div>
            <Background />
            <Footer />
        </>
    )
}

const mapStateToProps = (state) => {
    return {
        tab: state.mainReducer.tab,
        isError: state.mainReducer.isError,
        errorText: state.mainReducer.errorText
    }
}

export default connect(mapStateToProps,{ setError, setErrorText})(SignUpPage);