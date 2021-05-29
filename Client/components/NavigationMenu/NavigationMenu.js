import React from "react";
import PropTypes from "prop-types";
import "./style.css";
import Page from "../../constants/Page";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

export default function NavigationMenu({ onNavigateToPage, auth }) {
  return (
    <div className="main-menu">
      <div className="main-menu__inner" onClick={(event) => event.target.id !== '' ? onNavigateToPage(event.target.id) : null}>
        <Link id={Page.articles.text} className="block-menu lefter" to={Page.articles.route}>
          <span className="menu-text">Статьи</span>
        </Link>
        <Link id={Page.notes.text} style={auth ? {
          'backgroundImage': 'url(/src/img/lock.png)', 'backgroundColor': 'rgb(25, 50, 90)',
          'pointerEvents': 'none'
        } : { 'backgroundImage': 'url(/src/img/notes.png)' }}
          className="block-menu left" to={Page.notes.route}>
          <span className="menu-text">Заметки</span>
        </Link>
        <Link id={Page.learningMenu.text} className="block-menu center" to={Page.learningMenu.route}>
          <div className="explainer">
            <span id={Page.learningMenu.text}> Наведи на меня</span>
          </div>
          <span className="menu-text">Обучение</span>
        </Link>
        <Link id={Page.testsMenu.text} style={auth ? {
          'backgroundImage': 'url(/src/img/lock.png)', 'backgroundColor': 'rgb(25, 50, 90)',
          'pointerEvents': 'none'
        } : { 'backgroundImage': 'url(/src/img/test.png)' }}
          className="block-menu right " to={Page.testsMenu.route}>
          <span className="menu-text">Тесты</span>
        </Link>
        <Link id={Page.authors.text}
          className="block-menu righter" to={Page.authors.route}>
          <span className="menu-text">Авторы</span>
        </Link>
      </div>
    </div>
  );
}

NavigationMenu.propTypes = {
  onNavigateToPage: PropTypes.func.isRequired,
  auth: PropTypes.bool.isRequired
};
