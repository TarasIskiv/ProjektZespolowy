import Header from "../components/Header";
import Footer from "../components/Footer";
import Background from "../components/Background";
import SignForm from "../components/SignForm";
import {setAuth, setError, setErrorText, setRole} from "../actions/MainActions";
import {connect} from "react-redux";
import { login } from "../api/UserApi";
import {useEffect} from "react";
import UserApi from "../api/UserApi";
import CompanyApi from "../api/CompanyApi";
import Cookies from "universal-cookie";
import style from "../styles/pages/signPage.scss"

const SignInPage = (props) => {
    const fields = [
        { id: '1', name: 'email', title: 'Email', type: 'text' },
        { id: '2', name: 'password', title: 'Password', type: 'password' },
    ]

    const onSend = (data) => {
        props.setError(false);

        for(const key in data) {
            if(data[key] === '') {
                props.setError(true);
                props.setErrorText(key + " cannot be empty");
                return;
            }
        }

        const send = props.tab ?  CompanyApi.login : UserApi.login;

        send(data)
            .then((result) => {
                console.log(result);
                    props.setError(false);
                    //alert("jwt token - " + result.data.data);
                    const cookies = new Cookies();
                    cookies.set('jwt', result.data.data);
                    window.open('/profile', '_self');
            })
            .catch((error) => {
                console.log(error)
                if(error.response === undefined)
                {
                    props.setErrorText('The request was not sent');
                    props.setError(true);
                    return;
                }
                if(error.response.status === 500) {
                    props.setErrorText('Server error 500!');
                    props.setError(true);
                } else {
                    const errors = error.response.data.errors;
                    props.setErrorText('error');
                    props.setError(true);
                }
            });
    }

    const setTitle = (tab) => {
        const type = (tab ? 'Employer' : 'Employee');
        return type + ' login';
    }

    useEffect(() => {
        props.setError(false);

    },[props.tab])

    return (
        <>
            <Header />
            <div className="container">
                <SignForm
                    title={setTitle(props.tab)}
                    fields={fields}
                    onSend={onSend}
                    buttonTitle='Sign in'
                    error/>
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

export default connect(mapStateToProps, { setError, setErrorText, setRole })(SignInPage);