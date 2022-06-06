import React, { useState } from 'react'
import PropTypes from 'prop-types'
import { RiDeleteBin6Line } from 'react-icons/ri'
import { MdOutlineKeyboardArrowDown, MdOutlineKeyboardArrowUp } from 'react-icons/md'
import style from '../styles/components/offerRow.module.scss'
import JobApi from '../api/JobApi';

const OfferRow = ({ data, onDelete, isPublic}) => {
    const key = 1
    const [expanded, setExpanded] = useState(false);
    const isHide = (!expanded) ? style.hidden : style.showed;

    const arrowClick = () => setExpanded(!expanded);

    const buttonArrow = () => {
            if (expanded)
                return <MdOutlineKeyboardArrowUp size={20} onClick={arrowClick} />
            else
                return <MdOutlineKeyboardArrowDown size={20} onClick={arrowClick} />
    }

    return (
        <div className={style.offerRow}>
            <div className={style.info}>
                <div>
                    <p>Who:</p>
                    <input type="text" name={`who${key}`} id={`who${key}`} value={data.title} />
                </div>
                <div>
                    <p>Salary:</p>
                    <input type="text" name={`salary${key}`} id={`salary${key}`} value={data.salary} />
                </div>
                <div className={isHide}>
                    <p>Requirements:</p>
                    <textarea type="text" style={{width:'100%'}} name={`experience${key}`} id={`experience${key}`} value={data.requirements} />
                    <p>Description:</p>
                    <textarea type="text" style={{width:'100%'}} name={`desc${key}`} id={`desc${key}`} value={data.description}/>
                    <p>Responsobilites:</p>
                    <textarea type="text" style={{width:'100%'}} name={`respons${key}`} id={`respons${key}`} value={data.responsibilities} />
                </div>
            </div>
            {buttonArrow()}
            { !isPublic && <RiDeleteBin6Line size={20} onClick={() => onDelete(data.id)}/> }
        </div>
    )
}

OfferRow.propTypes = {}

export default OfferRow