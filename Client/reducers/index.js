import { combineReducers } from 'redux';
import { createReducer } from 'redux-create-reducer';
import Page from '../constants/Page';
import Status from '../constants/Status';
import * as actionTypes from '../actionTypes';


const pageReducer=createReducer(Page.mainMenu.text,{[actionTypes.NAVIGATE_TO_PAGE]:(state,action)=>action.page});

const accordionMenuReducer=createReducer({inf:[], status: Status.empty},
     {[actionTypes.LOAD_ACCORDION_MENU_INF_SUCCESS]:(state,action)=>({...state, inf:action.value, status: Status.loaded}),
     [actionTypes.LOAD_ACCORDION_MENU_INF_REQUEST]:(state,action)=>({...state, status: Status.loading})});

 const learningAreaReducer=createReducer({text:'Выберите интересующий вас раздел для обучения', status: Status.empty},
     {[actionTypes.LOAD_LEARNING_TEXT_SUCCESS]:(state,action)=>({...state, text:action.text, status: Status.loaded}),
     [actionTypes.LOAD_LEARNING_TEXT_REQUEST]:(state,action)=>({...state, status: Status.loading}),
    [actionTypes.SET_LEARNING_TEXT_DEFAULT]:(state,action)=>({text:'Выберите интересующий вас раздел для обучения', status: Status.empty})});

     
 const articlesListReducer=createReducer({articles:[], status: Status.empty},
    {[actionTypes.LOAD_ARTICLES_SUCCESS]:(state,action)=>({...state, articles:action.articles, status: Status.loaded}),
    [actionTypes.LOAD_ARTICLES_REQUEST]:(state,action)=>({...state, status: Status.loading})});

const userInformationReducer = createReducer({},{[actionTypes.SET_USER_INFORMATION]:(state,action)=>action.data});

const notesReducer=createReducer({notes:[], status: Status.empty},
    {[actionTypes.LOAD_NOTES_SUCCESS]:(state,action)=>({...state, notes:action.notes, status: Status.loaded}),
    [actionTypes.LOAD_NOTES_REQUEST]:(state,action)=>({...state, status: Status.loading})});

const testsReducer=createReducer({tests:[], status: Status.empty},
     {[actionTypes.LOAD_TESTS_SUCCESS]:(state,action)=>({...state, tests:action.tests, status: Status.loaded}),
     [actionTypes.LOAD_TESTS_REQUEST]:(state,action)=>({...state, status: Status.loading})});

const testReducer=createReducer({test:[], status: Status.empty},
    {[actionTypes.LOAD_TEST_SUCCESS]:(state,action)=>({...state, test:action.test, status: Status.loaded}),
    [actionTypes.LOAD_TEST_REQUEST]:(state,action)=>({...state, status: Status.loading})});

const testResultReducer = createReducer('',{[actionTypes.SET_TEST_RESULT]:(state,action)=>action.result});
   

export const rootReducer = combineReducers({
    page: pageReducer,
    accordionMenu: accordionMenuReducer,
    learningArea: learningAreaReducer,
    articlesList: articlesListReducer,
    userInformation: userInformationReducer,
    notesList: notesReducer,
    testsList: testsReducer,
    test: testReducer,
    testResult: testResultReducer
});