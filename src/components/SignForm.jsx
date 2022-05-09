import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import Button from './Button';

export const SignForm = ({ title, fields, onSend, buttonTitle, isError, errorText }) => {
  const [data, setData] = useState({});

  const onChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  }

  useEffect(() => {
    const tempData = {};
    fields.forEach(element => {
      tempData[element.name] = '';
    });
    setData(tempData);
    return () => setData({});
  }, [JSON.stringify(fields)]);

  return (
    <div className='form-section'>
      <h1>{title}</h1>
        <div className={isError ? 'error' : 'hidden'}><p>{errorText}</p></div>
        {fields.map((item) =>
          <div key={item.id}>
            <p>{item.title}</p>
            <label htmlFor={item.name}></label>
            <input 
              type={item.type} 
              value={data[item.name]} 
              name={item.name} 
              id={item.name} 
              onChange={onChange} 
              autoComplete="off" />
          </div>
        )}
        <Button onClick={() => onSend(data) } text={buttonTitle} />
    </div>
  )
}

const mapStateToProps = (state) => {
    return {
        isError: state.mainReducer.isError,
        errorText: state.mainReducer.errorText
    }
}

export default connect(mapStateToProps)(SignForm);