import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link, useRouteMatch } from 'react-router-dom'
import Status from '../../constants/Status';
import Page from "../../constants/Page";
import Loader from "../../containers/Loader"
import './style.css';

export default function Articles({ articlesList, onNavigateToPage, setLearningText, requestLearningText }) {
  if (articlesList.status !== Status.loaded) return <Loader fontColor='#fff' />;
  const openArticle = async (event) => {
    requestLearningText();
    const text =await (await fetch(`/api/articles/${event.target.id}`)).text();
    //const text = 'Статьи';
    setLearningText(text);
  };

  const articlesForm = [];
  for (let el of articlesList.articles) {
    articlesForm.push(<Link className='article-ref' onClick={openArticle}  key={el.id} to={`${Page.articles.route}/${el.id}`}>
      <article  id={el.id} className='article-item'>
      <div className='image' id={el.id}  style={{ 'backgroundImage': `url(${el.image})` }}></div>
      <div id={el.id} className='article-data'>
        <div className='title' id={el.id}>{el.title}</div>
        <div id={el.id} className className='description'>{el.description}</div>
        <div id={el.id} className='info'>{el.date} / {el.author}</div>
      </div></article></Link>)
  }
  return (
    <div className='main-articles-menu'>
      <div className='nav'>
        <Link to={Page.mainMenu.route}> <div onClick={() => { onNavigateToPage(Page.mainMenu.text) }}
          className='back-ref'>&#11013; Вернуться</div></Link></div>
      <div className='articles-menu'>
        <div className='articles-menu-list' onClick={(event) => onNavigateToPage('Статья № ' + event.target.id)}>
          {articlesForm}
        </div>
      </div>
    </div>

  )
}

Articles.propTypes = {
  articlesList: PropTypes.object.isRequired,
  onNavigateToPage: PropTypes.func.isRequired,
  requestLearningText: PropTypes.func.isRequired,
  setLearningText: PropTypes.func.isRequired,
}
