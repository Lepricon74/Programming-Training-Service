import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import Page from "../../constants/Page";
import Status from '../../constants/Status';
import Loader from "../../containers/Loader"
import Modal from "../../containers/Modal"
import './style.css';

export default function LearningArea({ learningArea, pageIndication }) {
    return (
        <div className='learning-content'>
            <div className='learning-text' style={pageIndication ? { 'minWidth': '85%' } : { 'minWidth': '65%' }} >
                <a title='Добавить заметку' href='#modal' className='button-note'></a>
                {learningArea.status === Status.loading ? <Loader fontColor='black' /> :
                    <div className='text' dangerouslySetInnerHTML={{ __html: learningArea.text }} ></div>}</div>
            <Modal />
        </div>
    )
}

LearningArea.propTypes = {
    learningArea: PropTypes.object.isRequired,
    pageIndication: PropTypes.bool.isRequired
};