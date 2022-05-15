import { Navigate } from "react-router-dom";
import Cookies from "universal-cookie";
import jwt_decode from "jwt-decode";
import {clearMainState, setRole} from "../actions/MainActions";
import {connect} from "react-redux";

const CheckAuth = (props) => {
    const cookies = new Cookies();
    const token = cookies.get('jwt');
    if(token === undefined) {
        props.clearMainState();
        return <Navigate  to="/" />
    }

    const decodedJwt = jwt_decode(token);
    const dateNow = new Date();
    if(decodedJwt.exp * 1000 < Date.now()) {
        props.clearMainState();
        return <Navigate  to="/" />
    }
    props.setRole(decodedJwt.role);

    return props.children;
}

export default connect(null,{ setRole, clearMainState })(CheckAuth);