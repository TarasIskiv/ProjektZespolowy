import React from 'react'
import Button from './Button'
import PropTypes from 'prop-types'
import style from '../styles/components/searchOfferRow.module.scss'
import { AiOutlineMail, AiOutlineStar } from "react-icons/ai";
import { FiPhone } from 'react-icons/fi'
const SearchOfferRow = props => {
    return (
        <div className={style.row}>
            <img src="https://jf-staeulalia.pt/img/other/23/collection-apple-logo-outline.jpg" alt="Logo" />
            <div className={style.info}>
                <span>Google Inc.</span>
                <span>Senior .Net developer</span>
                <span>Salary: 4000$/month</span>
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

export default SearchOfferRow