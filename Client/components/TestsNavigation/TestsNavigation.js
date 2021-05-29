import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link, useRouteMatch } from 'react-router-dom'
import Status from '../../constants/Status';
import Page from "../../constants/Page";
import Loader from "../../containers/Loader"
import './style.css';
import testQuestions from "../../forTests/testQuestions";

export default function TestsNavigation({ onNavigateToPage, testsList, page, requestTest, setTest, setTestResult }) {
  if (testsList.status !== Status.loaded) return <Loader fontColor='#fff' />;
  const testForm = [];
  const loadTest = async (event) => {
    setTestResult('');
    let route;
    switch (page) {
      case Page.testsSharp.text:
        route = 'sharp';
        break;
      case Page.testsJS.text:
        route = 'js';
        break;
      case Page.testsSQL.text:
        route = 'sql';
        break;
      default:
        break;
    }
    requestTest();
    const test=await (await fetch(`/api/tests/${route}/${event.target.id}`)).json();
    //const test = testQuestions;
    setTest(test);
  }

  const setRoute = (el) => {
    let route;
    switch (page) {
      case Page.testsSharp.text:
        route = Page.testsSharp.route;
        break;
      case Page.testsJS.text:
        route = Page.testsJS.route;
        break;
      case Page.testsSQL.text:
        route = Page.testsSQL.route;
        break;
      default:
        break;
    }
    return `${route}/${el.id}`;
  }
  for (let el of testsList.tests) {
    testForm.push(<Link className='test-ref' onClick={loadTest} key={el.id} to={setRoute(el)}><div id={el.id} className='test-item'>
      <div className='image' id={el.id} style={{ 'backgroundImage': `url(${el.image})` }}></div>
      <div className='test-inf' id={el.id} ><div id={el.id} className='test-title'>{el.title}</div>
        <div className='test-rating' id={el.id} >Ваш прогресс - {el.rating} %</div></div>
    </div></Link>)
  }


  return (
    <div className='main-tests-navigation'>
      <div className='nav'>
        <Link to={Page.testsMenu.route}> <div onClick={() => { onNavigateToPage(Page.testsMenu.text) }}
          className='back-ref'>&#11013; Вернуться</div></Link></div>
      <div className='tests-navigation'>
        <div className='tests-list' onClick={(event) => onNavigateToPage(page + ' № ' + event.target.id)}>
          {testForm}
        </div>
      </div>
    </div>
  )
}

TestsNavigation.propTypes = {
  onNavigateToPage: PropTypes.func.isRequired,
  testsList: PropTypes.object.isRequired,
  page: PropTypes.string.isRequired,
  setTest: PropTypes.func.isRequired,
  requestTest: PropTypes.func.isRequired,
  setTestResult: PropTypes.func.isRequired
}
