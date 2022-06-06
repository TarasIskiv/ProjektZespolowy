import logo from '../resources/logo.svg'
import Button from './Button';
import Switch from './Switch';
import {Link} from "react-router-dom";
import style from '../styles/components/header.module.scss'
import {useState} from 'react';
import Hamburger from './Hamburger';
import {connect} from "react-redux";


const Header = (props) => {

  const [open, setOpen] = useState(0);

  const onClick = (whichOne) => { setOpen(whichOne); }

  const menuOpened = open ? style.open_menu : "";

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
              <>
                <Switch onSwitch={props.onSwitch} className={style.switch}/>
                <div className={style.buttons}>
                  <Link to='/signup'><Button text="Register"/></Link>
                  <Link to='/signin'><Button text="Sign in"/></Link>
                </div>
              </>
              // :
              // <>
              // <div style={{width:'100%'}}></div>
              //   <Link to='/profile'><Button text="Profile" /></Link>
              // </>
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

export default connect(mapStateToProps, null)(Header);