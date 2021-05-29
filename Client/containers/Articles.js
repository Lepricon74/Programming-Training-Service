import React from 'react';
import { connect } from 'react-redux';
import Articles from '../components/Articles/Articles';
import { navigateToPage, setLearningText, requestLearningText } from '../actionCreators';

export default connect(
    (state, props) => ({
        articlesList: state.articlesList
    }),
    (dispatch, props) => ({
        onNavigateToPage: value => dispatch(navigateToPage(value)),
        setLearningText: text => dispatch(setLearningText(text)),
        requestLearningText: () => dispatch(requestLearningText()),
    })
)(Articles);