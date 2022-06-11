import React from 'react'
import Button from './Button'
import PropTypes from 'prop-types'
import style from '../styles/components/searchOfferRow.module.scss'
import { AiOutlineMail, AiOutlineStar } from "react-icons/ai";

import { FiPhone } from 'react-icons/fi';
import { setActiveJob } from '../actions/JobActions';
import {connect} from "react-redux";

const SearchOfferRow = props => {
    const onClick = () => {
        const data = {...props};

        props.setActiveJob(data);
        console.log(props)
    }

    return (
        <div className={style.row} onClick={onClick}>
            <img src="https://jf-staeulalia.pt/img/other/23/collection-apple-logo-outline.jpg" alt="Logo" />
            <div className={style.info}>
                <span>{props.title}</span>
                <span>{props.company.companyName}</span>
                <span>Salary: {props.salary}$/month</span>
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

SearchOfferRow.propTypes = {}

export default connect(null, { setActiveJob })(SearchOfferRow)

