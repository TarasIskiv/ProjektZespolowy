import { combineReducers } from "redux";
import mainReducer from "./MainReducer";
import companyReducer from "./CompanyReducer";
import jobReducer from "./JobReducer";
import employeeReducer from "./EmployeeReducer";

export default combineReducers({
    mainReducer: mainReducer,
    companyReducer: companyReducer,
    jobReducer: jobReducer,
    employeeReducer: employeeReducer
});