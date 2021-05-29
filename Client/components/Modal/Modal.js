import React from 'react';
import PropTypes from 'prop-types';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom'
import './style.css';
import Page from "../../constants/Page"; 

export default function Modal({page, requestNotes, setNotes}) {
    const submit = async (event) => {
        event.preventDefault();
        await fetch('/api/notes/addnote', {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              title: document.getElementById('note-title').value,
              text: document.getElementById('note-text').value
            })
        });
        if (page === Page.notes.text){
            requestNotes();
            let notes=await (await fetch('/api/notes')).json();
            setNotes(notes);
        }
        document.location.href = '#close';
        document.getElementById('note-title').value = '';
        document.getElementById('note-text').value = '';

    };
    return (
        <div id='modal' className='modal'>
            <div className='modal-wrapper'>
                <div className='modal-inner'>
                    <div className='note-header'>Создание новой заметки</div>
                    <form className='modal-content' onSubmit={submit}>
                        <a title='Закрыть' href='#close' className='close-note'>X</a>
                        <input id='note-title' maxLength='40' required className='note-title' placeholder='Введите название заметки'></input>
                        <textarea id='note-text' maxLength='500' required className='note-text' placeholder='Введите текст заметки'></textarea>
                        <input value='Сохранить заметку' type='submit' className='save-note'></input>
                    </form>
                </div>
            </div>
        </div>
    )
}

Modal.propTypes = {
page: PropTypes.string.isRequired,
setNotes: PropTypes.func.isRequired,
requestNotes: PropTypes.func.isRequired
};