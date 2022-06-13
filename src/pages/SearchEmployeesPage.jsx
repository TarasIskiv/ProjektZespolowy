import Header from "../components/Header";
import Background from "../components/Background";
import Footer from "../components/Footer";
import styles from '../styles/pages/SearchPage.module.scss';
import {BiFilterAlt} from "react-icons/bi";
import {BsSearch} from "react-icons/bs";
import {MdSort} from "react-icons/md";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router";
import EmployeeApi from '../api/EmployeeApi';
import SearchEmployeeRow from "../components/SearchEmployeeRow";
import {connect} from "react-redux";
import {setEmployee} from "../actions/EmployeeAction";


const SearchEmployeesPage = (props) => {
    const navigate = useNavigate();
    const [dropdown, setDropDown] = useState(false);

    const [experience, setExperience] = useState('');
    const [city, setCity] = useState('');
    const [country, setCountry] = useState('');
    const [search, setSearch] = useState('');
    const [gender, setGender] = useState('');

    const onClick = (e) => {
        setDropDown(!dropdown);
    }

    const generateParams = () => {
        const params = {};
        if(experience !== '')
            params['experience'] = experience;

        if(country !== '')
            params['country'] = country;

        if(city !== '')
            params['city'] = city;

        if(search !== '')
            params['search'] = search;

        if(gender !== '')
            params['gender'] = gender;

        return params;
    }

    useEffect(() => {
        const urlParams = new URLSearchParams(generateParams()).toString();
        EmployeeApi.searchEmployees(urlParams).then(res => {
            console.log(res.data.data);
            props.setEmployee(res.data.data);
        }).catch(error => {
            props.setEmployee(null);
            props.setActiveEmployee(null);
        });

        navigate({
            pathname: window.location.pathname,
            search: '?' + urlParams
        })

        console.log(props)
    },[experience, country, city, search, gender]);


    useEffect(async () => {
        const urlParams = new URLSearchParams(generateParams()).toString();
        EmployeeApi.searchEmployees(urlParams).then(res => {
            console.log(res.data.data);
            props.setEmployee(res.data.data);
        }).catch(error => {
            props.setEmployee(null);
            props.setActiveEmployee(null);
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
                                <p>Experience</p>
                                <input type='number' value={experience} onChange={(e) => setExperience(e.target.value)} />
                            </div>
                            <div>
                                <p>Country</p>
                                <input type='text' value={country} onChange={(e) => setCountry(e.target.value)} />
                            </div>
                            <div>
                                <p>City</p>
                                <input type='text' value={city} onChange={(e) => setCity(e.target.value)}/>
                            </div>
                            <div>
                                <p>Gender</p>
                                <input type='text' value={gender} onChange={(e) => setGender(e.target.value)} />
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
                        { props.activeEmployee != null ? <>
                            <img src={props.activeEmployee.image} />
                        <h2>{props.activeEmployee.firstName + " " + props.activeEmployee.lastName}</h2>
                        <span>{props.activeEmployee.experience} years</span>

                        <h1>Description:</h1>
                        <p>{props.activeEmployee.desciption}</p>

                        <h1>Email:</h1>
                        <p>{props.activeEmployee.email}</p>

                        <h1>Phone:</h1>
                        <p>{props.activeEmployee.contactNumber}</p>

                        </>
                            : <></>
                        }

                    </div>
                    <div className={styles.right}>
                        {
                            (props.employees != null) ? 
                            props.employees.map(item => {
                                return <SearchEmployeeRow {...item} />
                            })
                            : <></>
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
        employees: state.employeeReducer.employees,
        activeEmployee: state.employeeReducer.activeEmployee
    }
}

export default connect(mapStateToProps, { setEmployee })(SearchEmployeesPage);