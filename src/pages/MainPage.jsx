import Alert from '@mui/material/Alert';

import Header from '.././components/Header';
import Button from '.././components/Button'
import Photo from '../resources/job-interview.png'
import Footer from "../components/Footer";
import { useState } from "react";
import Background from '../components/Background';
import {switchTab} from "../actions/MainActions";
import {connect} from "react-redux";
import style from '../styles/pages/mainPage.scss'
import {Link} from "react-router-dom";
import SearchOfferRow from '../components/SearchOfferRow';

const MainPage = (props) => {

  return (
    <>
      <Header />
      <main>
        <div className="container">
        <section className='first-section'>
            <img src="https://thispersondoesnotexist.com/image" alt="A human being" />
            <div>
              <h1>findjob works for you.</h1>
              {!props.tab ? <Link to='/creatorcv'><Button fontsize='24px' text='Create CV' /> </Link>: <input placeholder='Search' />}
            </div>
        </section>
        <section className='second-section'>
            <div>
              <h1>sit amet purus gravida quis</h1>
              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            </div>
            <img src={Photo} />
        </section>
      </div>
      </main>
      <Background />
      <Footer />
    </>
  );
}

const mapStateToProps = (state) => {
    return {
        tab: state.mainReducer.tab
    }
}

export default connect(mapStateToProps, { switchTab})(MainPage);