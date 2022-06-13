import logo from '../resources/logo.svg'
import Button from './Button';
import Switch from './Switch';
import {Link} from "react-router-dom";
import style from '../styles/components/header.module.scss'
import {useState} from 'react';
import Hamburger from './Hamburger';
import {connect} from "react-redux";
import Cookie from 'universal-cookie';
import { setRole } from '../actions/MainActions';

const Header = (props) => {

  const [open, setOpen] = useState(0);

  const onClick = (whichOne) => { setOpen(whichOne); }

  const menuOpened = open ? style.open_menu : "";

  const useEffect = (() => {
  }, [props.role]);

  const signOut = () => {
    let cookie = new Cookie();
    console.log(props.role);
    props.setRole(null);
    window.open('/', '_self');
  }

  return (
    <header className={style.header}>
      <div className={style.container}>
        <div className={style.header_wrap}>
          <Link to='/' className={style.logo}>
            <img src={logo} height='38px' alt="findjob."  id="logo" />
          </Link>
          <Hamburger className={style.hamburger} onClick={() => onClick(!menuOpened)} />
        </div>
        <div className={[style.header_main, menuOpened].join(" ")}>
          {
            props.role == null ?
              <>
                <Switch onSwitch={props.onSwitch} className={style.switch}/>
                <div className={style.buttons}>
                  <Link to='/signup'><Button text="Register"/></Link>
                  <Link to='/signin'><Button text="Sign in"/></Link>
                </div>
              </>
              :
              <>
              {/* <input placeholder='Search' onKeyDown={onKeyDownSearch}/> */}
              <div className={[style.buttons, style.search].join(" ")}></div>
              <div className={style.buttons}>
                {
                  props.role == 'User' ?
                  <>
                    <Link to="/search-offers"><Button text="Search offers" /></Link>
                    <Link to=""><Button text="Search companies" /></Link>
                  </>
                  :
                  <>
                    <Link to="/search-employees"><Button text="Search employees" /></Link>
                  </>
                }
                <Link to='/profile'><Button text="Profile" /></Link>
                <Button text="Sign out" onClick={signOut} />
              </div>
              </>
          }
        </div>

      </div>
    </header>
  );
}

const mapStateToProps = (state) => {
  return {
    role: state.mainReducer.role
  };
}

export default connect(mapStateToProps, {setRole})(Header);