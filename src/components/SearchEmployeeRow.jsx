import React from 'react'
import Button from './Button'
import PropTypes from 'prop-types'
import style from '../styles/components/searchOfferRow.module.scss'
import { AiOutlineMail, AiOutlineStar } from "react-icons/ai";

import { FiPhone } from 'react-icons/fi';
import { setActiveEmployee } from '../actions/EmployeeAction';
import {connect} from "react-redux";

const SearchEmployeeRow = props => {
    const onClick = () => {
        const data = {...props};

        props.setActiveEmployee(data);
        console.log(props)
    }

    return (
        <div className={style.row} onClick={onClick}>
            <img src={props.image} />
            <div className={style.info}>
                <span>{props.firstName + " " + props.lastName}</span>
                <span>{props.desciption}</span>
                <span>Experience: {props.experience} years</span>
            </div>
            <div className={style.button}>
                <Button>
                    <AiOutlineMail size={20} />
                </Button>
                <Button>
                    <FiPhone size={20} />
                </Button>
                <Button>
                    <AiOutlineStar size={20} />
                </Button>
            </div>
        </div>
    )
}

SearchEmployeeRow.propTypes = {}

export default connect(null, { setActiveEmployee })(SearchEmployeeRow)

