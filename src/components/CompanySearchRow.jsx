import React from 'react'
import Button from './Button'
import PropTypes from 'prop-types'
import style from '../styles/components/searchOfferRow.module.scss'
import { AiOutlineMail, AiOutlineStar } from "react-icons/ai";

import { FiPhone } from 'react-icons/fi';
import { setActiveJob } from '../actions/JobActions';
import {connect} from "react-redux";

const CompanySearchRow = props => {
    return (
        <div className={style.row} onClick={props.onClick}>
            <img src={props.image} alt="Logo" />
            <div className={style.info}>
                <span>{props.companyName}</span>
            </div>
        </div>
    )
}

CompanySearchRow.propTypes = {}

export default connect(null, { setActiveJob })(CompanySearchRow)

