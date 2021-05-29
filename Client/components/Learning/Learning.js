import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import Page from "../../constants/Page";
import AccordionMenu from "../../containers/AccordionMenu";
import './style.css';

export default function Learning({ setLearningTextDefault, onNavigateToPage }) {
  return (
    <div className='main-learning-page'>
      <div className='nav'>
        <Link to={Page.learningMenu.route}> <div onClick={() => { onNavigateToPage(Page.learningMenu.text); setLearningTextDefault() }}
          className='back-ref'>&#11013; Вернуться</div></Link></div>
      <AccordionMenu />
    </div>
  )
}

Learning.propTypes = {
  setLearningTextDefault: PropTypes.func.isRequired,
  onNavigateToPage: PropTypes.func.isRequired
};