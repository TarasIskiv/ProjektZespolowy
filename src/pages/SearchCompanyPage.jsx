import Header from "../components/Header";
import EmployerPage from "./EmployerPage";
import EmployeePage from "./EmployeePage";
import Background from "../components/Background";
import Footer from "../components/Footer";
import styles from '../styles/pages/SearchPage.module.scss';
import {BiFilterAlt} from "react-icons/bi";
import {BsSearch} from "react-icons/bs";
import {MdSort} from "react-icons/md";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router";
import CompanyApi from '../api/CompanyApi';
import SearchOfferRow from "../components/SearchOfferRow";
import {connect} from "react-redux";
import {setJob} from "../actions/JobActions";
import CompanySearchRow from "../components/CompanySearchRow";


const SearchOffersPage = (props) => {
    const navigate = useNavigate();
    const [dropdown, setDropDown] = useState(false);

    const [companies, setCompanies] = useState(null);
    const [activeCompany, setActiveCompany] = useState(null);
    const [search, setSearch] = useState('');

    const onClick = (e) => {
        setDropDown(!dropdown);
    }

    useEffect( () => {
        CompanyApi.getAll().then(res => {
            console.log(res.data.data);
            setCompanies(res.data.data);
        });

    },[])

    return (
        <>
            <Header />
            <div className="container">
                <div className="main-block">
                    <div className={styles.top}>
                        <div className={styles.search}>
                            <input placeholder='Search' value={search} onChange={(e) => setSearch(e.target.value)}/>
                        </div>
                    </div>
                    <div className={styles.main}>
                        <div className={styles.left}>
                            { activeCompany !== null ? <>
                                    <img src={activeCompany.image} />
                                    <h2>{activeCompany.title}</h2>
                                    <a href={`/company/${activeCompany.id}`}>{activeCompany.companyName}</a>
                                    <p>{activeCompany.description}</p>
                               </>
                                : <></>
                            }

                        </div>
                        <div className={styles.right}>
                            {
                                companies !== null ? companies.map(item => {
                                    return <CompanySearchRow {...item} onClick={() => {
                                        console.log(item);
                                        setActiveCompany(item)
                                    }}/>
                                }) : <></>
                            }
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
        jobs: state.jobReducer.jobs,
        activeJob: state.jobReducer.activeJob
    }
}

export default connect(mapStateToProps, { setJob })(SearchOffersPage);