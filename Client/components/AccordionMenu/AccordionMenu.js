import React from 'react';
import PropTypes, { object } from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import Page from "../../constants/Page";
import Status from '../../constants/Status';
import Loader from "../../containers/Loader"
import './style.css';
import LearningArea from '../../containers/LearningArea';

export default function AccordionMenu({ onNavigateToPage, accordionMenu, setLearningText, requestLearningText, page }) {
  if (accordionMenu.status !== Status.loaded) return <Loader fontColor='#fff' />
  const loadText = async (event) => {
    event.stopPropagation()
    const ids = event.target.id.split(',');
    requestLearningText();
    let route;
    switch (page) {
      case Page.learningSharp.text:
        route = 'sharp';
        break;
      case Page.learningJS.text:
        route = 'js';
        break;
      case Page.learningSQL.text:
        route = 'sql';
        break;
      default:
        break;
    }
    const text =await (await fetch(`/api/lessons/${route}/${ids[0]}/${ids[1]}`)).text();
    //const text = 'dsfd';
    setLearningText(text);

  };
  const menu = [];
  for (let el of accordionMenu.inf) {
    menu.push(<div key={el.id} className='element' onClick={() => { document.getElementById(el.sectionName).classList.toggle('open-sub-menu') }}>
      <a className='elem-title'>{el.sectionName}</a>
      <div className='sub-menu' id={el.sectionName}>
        {el.lessons.map(function (obj, i) {
          return <a key={obj.id} id={obj.sectionId + ',' + obj.id} onClick={loadText}>{obj.name}</a>
        })}
      </div>
    </div>)
  }
  return (
    <div className='main-learning-container'>
      <div className='learning-container'>
        <div className='menu'>
          {menu}
        </div>
      </div>
      <LearningArea pageIndication={true} />
    </div>
  )
}

AccordionMenu.propTypes = {
  onNavigateToPage: PropTypes.func.isRequired,
  accordionMenu: PropTypes.object.isRequired,
  requestLearningText: PropTypes.func.isRequired,
  setLearningText: PropTypes.func.isRequired,
  page: PropTypes.string.isRequired,

};