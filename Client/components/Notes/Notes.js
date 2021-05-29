import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom'
import './style.css';
import Loader from "../../containers/Loader";
import Status from '../../constants/Status';
import Page from "../../constants/Page";
import Modal from "../../containers/Modal"

export default function Notes({ notesList, onNavigateToPage }) {
    if (notesList.status !== Status.loaded) return <Loader fontColor='#fff' />
    const notesForm = [];
    for (let note of notesList.notes) {
        notesForm.push(<div key={note.id} className='note-form'><div className='note-title'>{note.title}</div>
            <div className='note-text'>{note.text}</div></div>)
    }
    return (
        <div className='main-notes-field'>
            <div className='nav'><Link to={Page.mainMenu.route}><div onClick={() => { onNavigateToPage(Page.mainMenu.text) }} className='back-ref'>&#11013; Вернуться</div></Link>
                <a href='#modal'><div title='Создать новую заметку' className='create-ref'>&#10010; Создать</div> </a></div>
            <div className='notes-field'>
                {notesForm}
            </div>
            <Modal />
        </div>

    )
}

Notes.propTypes = {
    notesList: PropTypes.object.isRequired,
    onNavigateToPage: PropTypes.func.isRequired
};