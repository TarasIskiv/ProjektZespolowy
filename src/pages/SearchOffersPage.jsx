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
import JobApi from '../api/JobApi';
import SearchOfferRow from "../components/SearchOfferRow";
import {connect} from "react-redux";
import {setJob} from "../actions/JobActions";


const SearchOffersPage = (props) => {
    const navigate = useNavigate();
    const [dropdown, setDropDown] = useState(false);

    const [minSalary, setMinSalary] = useState('');
    const [city, setCity] = useState('');
    const [country, setCountry] = useState('');
    const [search, setSearch] = useState('');

    const onClick = (e) => {
        setDropDown(!dropdown);
    }

    const generateParams = () => {
        const params = {};
        if(minSalary !== '')
            params['minSalary'] = minSalary;

        if(country !== '')
            params['country'] = country;

        if(city !== '')
            params['city'] = city;

        if(search !== '')
            params['search'] = search;


        return params;
    }

    useEffect(() => {
        const urlParams = new URLSearchParams(generateParams()).toString();
        JobApi.searchOffers(urlParams).then(res => {
            console.log(res.data.data);
            props.setJob(res.data.data);
        });

        navigate({
            pathname: window.location.pathname,
            search: '?' + urlParams
        })

        console.log(props)
    },[minSalary, country, city, search]);


    useEffect(async () => {
        const urlParams = new URLSearchParams(generateParams()).toString();
        JobApi.searchOffers(urlParams).then(res => {
            console.log(res.data.data);
            props.setJob(res.data.data);
        });

    },[])

    return (
    <>
        <Header />
        <div className="container">
            <div className="main-block">
                <div className={styles.top}>
                    <div className={` ${(dropdown ? styles.onClick : '')} ${styles.filter}`} >
                        <p>Filter</p>
                        <BiFilterAlt onClick={onClick} />
                        <div className={`${(!dropdown ? styles.noneMenu : '')} ${styles.dropdown}`} >
                            <div>
                                <p>Minimal salary</p>
                                <input type='text' value={minSalary} onChange={(e) => setMinSalary(e.target.value)} />
                            </div>
                            <div>
                                <p>Country</p>
                                <input type='text' value={country} onChange={(e) => setCountry(e.target.value)} />
                            </div>
                            <div>
                                <p>City</p>
                                <input type='text' value={city} onChange={(e) => setCity(e.target.value)}/>
                            </div>
                        </div>
                    </div>
                    <div className={styles.filter}>
                        <p>Sort by</p>
                        <MdSort />
                    </div>
                    <div className={styles.search}>
                        <input placeholder='Search' value={search} onChange={(e) => setSearch(e.target.value)}/>
                    </div>
                </div>
                <div className={styles.main}>
                    <div className={styles.left}>
                        { props.activeJob !== undefined ? <>
                            <img src='https://jf-staeulalia.pt/img/other/23/collection-apple-logo-outline.jpg' />
                        <h2>{props.activeJob.title}</h2>
                        <span>{props.activeJob.salary}$</span>

                        <h1>Description:</h1>
                        <p>{props.activeJob.description}</p>

                        <h1>Requirements:</h1>
                        <p>{props.activeJob.requirements}</p>

                        <h1>Responsibilities:</h1>
                        <p>{props.activeJob.responsibilities}</p>
                        </>
                            : <></>
                        }

                    </div>
                    <div className={styles.right}>
                        {
                            props.jobs.map(item => {
                                return <SearchOfferRow {...item} />
                            })
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