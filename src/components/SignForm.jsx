import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import Button from './Button';

//{ title, fields, onSend, buttonTitle, isError, errorText = '' }
export const SignForm = (props) => {
  const [data, setData] = useState({});

  const onChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  }

  useEffect(() => {
    const tempData = {};
      props.fields.forEach(element => {
      tempData[element.name] = '';
    });
    setData(tempData);
    return () => setData({});
  }, [JSON.stringify(props.fields)]);
    //console.log(props.fields)

  return (
    <div className='form-section'>
      <h1>{props.title}</h1>
        <div className={props.isError ? 'error' : 'hidden'}><p>{props.errorText}</p></div>
        {props.fields.map((item) =>
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
        <Button onClick={() => props.onSend(data) } text={props.buttonTitle} />
    </div>
  )
}

const mapStateToProps = (state) => {
    return {
        isError: state.mainReducer.isError,
        errorText: state.mainReducer.errorText
    }
}

export default connect(mapStateToProps, {})(SignForm);