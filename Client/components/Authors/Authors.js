import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom'
import "./style.css";
import Page from "../../constants/Page";

export default function Authors({onNavigateToPage}) {
    return (
        <div className='main-authors-container'>
            <div className='nav'>
        <Link to={Page.mainMenu.route}> <div onClick={() => { onNavigateToPage(Page.mainMenu.text) }}
          className='back-ref'>&#11013; Вернуться</div></Link></div>
        <div className='authors-container'>
            <div className='author-container'>
                <span className='author-name'>Коротких Вадим Юрьевич</span>
                <div className='author-photo' style={{'backgroundImage': 'url(/src/img/photo1.png)'}}></div>
                <span className='author-univer'>УрФУ, ИРИТ-РТФ, 4 курс, Информатика и вычислительная техника</span>
                <span className='author-work'>Создание клиентской части приложения. Настройка маршрутизации страниц. Реализация взаимодействия клиентской
                и серверной частей</span>
                </div>
            <div className='author-container'>
                <span className='author-name'>Бутузов Александр Витальевич</span>
                <div className='author-photo' style={{'backgroundImage': 'url(/src/img/photo2.png)'}}></div>
                <span className='author-univer'>УрФУ, ИРИТ-РТФ, 4 курс, Информатика и вычислительная техника</span>
                <span className='author-work'>Создание серверной части приложения. Проектирование API и базы данных. Реализация взаимодействия клиентской
                и серверной частей</span>
                </div>
        </div>
        </div>
    )
}

Authors.propTypes = {
    onNavigateToPage: PropTypes.func.isRequired
};