import React from "react";
import PropTypes from "prop-types";
import "./style.css";
import Page from "../../constants/Page";
import testAccordionMenu from "../../forTests/testAccordionMenu"
import testTests from "../../forTests/testTests";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import LearningNavigationText from '../../constants/LearningNavigationText'
import TestingNavigationText from '../../constants/TestingNavigationText'

export default function LanguageNavigation({ onNavigateToPage, setAccordionMenu, requestAccordionMenu, page, setTests, requestTests, setLearningTextDefault }) {
  const openMenu = async (event) => {
    let route;
    switch (event.target.id) {
      case Page.learningSharp.text:
      case Page.testsSharp.text:
        route = 'sharp';
        break;
      case Page.learningJS.text:
      case Page.testsJS.text:
        route = 'js';
        break;
      case Page.learningSQL.text:
      case Page.testsSQL.text:
        route = 'sql';
        break;
      default:
        break;
    }
    if (page === Page.learningMenu.text) {
      setLearningTextDefault();
      requestAccordionMenu();
      const menu=await (await fetch(`/api/lessons/${route}`)).json();
      //const menu = testAccordionMenu;
      setAccordionMenu(menu);
    }
    else {
      requestTests();
      const test = await (await fetch (`/api/tests/${route}`)).json();
      //const test = testTests;
      setTests(test);

    }
  };
  const text = (page === Page.learningMenu.text) ? LearningNavigationText : TestingNavigationText;
  const menuForm = [];
  for (let el of text) {
    menuForm.push(<li key={el.key} className='language-menu-list__item'>
      <Link id={el.page.text} onClick={openMenu} className='language-ref' to={el.page.route}>{el.text}</Link>
    </li>)
  }
  return (
    <div className='main-language-menu'>
      <div className='nav'>
        <Link to={Page.mainMenu.route}> <div onClick={() => { onNavigateToPage(Page.mainMenu.text) }}
          className='back-ref'>&#11013; Вернуться</div></Link></div>
      <div className='language-menu'>
        <ul className='language-menu-list' onClick={(event) => onNavigateToPage(event.target.id)}>
          {menuForm}
        </ul>
      </div>
    </div>
  );
}

LanguageNavigation.propTypes = {
  onNavigateToPage: PropTypes.func.isRequired,
  setAccordionMenu: PropTypes.func.isRequired,
  requestAccordionMenu: PropTypes.func.isRequired,
  page: PropTypes.string.isRequired,
  requestTests: PropTypes.func.isRequired,
  setTests: PropTypes.func.isRequired,
  setLearningTextDefault: PropTypes.func.isRequired
};