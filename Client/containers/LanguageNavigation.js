import React from 'react';
import { connect } from 'react-redux';
import LanguageNavigation from '../components/LanguageNavigation/LanguageNavigation';
import { navigateToPage,setAccordionMenuInf, requestAccordionMenuInf, setTests, requestTests, setLearningTextDefault} from '../actionCreators';

export default connect(
    (state,props) => ({
        page: state.page,

    }),
    (dispatch,props) =>({
        onNavigateToPage: value => dispatch(navigateToPage(value)),
        setAccordionMenu: value => dispatch(setAccordionMenuInf(value)),
        requestAccordionMenu: () => dispatch(requestAccordionMenuInf()),
        setTests: value => dispatch(setTests(value)),
        requestTests: () => dispatch (requestTests()),
        setLearningTextDefault: ()=> dispatch(setLearningTextDefault()),
    })
)(LanguageNavigation)