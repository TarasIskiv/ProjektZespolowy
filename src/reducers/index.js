import { combineReducers } from "redux";
import mainReducer from "./MainReducer";
import companyReducer from "./CompanyReducer";

export default combineReducers({
    mainReducer: mainReducer,
    companyReducer: companyReducer
});