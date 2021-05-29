import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import Page from "../../constants/Page";
import Status from '../../constants/Status';
import Loader from "../../containers/Loader";
import LearningArea from "../../containers/LearningArea";
import './style.css';

export default function ArticlePage({ onNavigateToPage, setLearningTextDefault }) {
    return (
        <div className='article-field'> <div className='nav'>
            <Link to={Page.articles.route}> <div onClick={() => { onNavigateToPage(Page.articles.text); setLearningTextDefault() }}
                className='back-ref'>&#11013; Вернуться</div></Link></div>
            <LearningArea pageIndication={false} /></div>
    )
}

ArticlePage.propTypes = {
    onNavigateToPage: PropTypes.func.isRequired,
    setLearningTextDefault: PropTypes.func.isRequired
};