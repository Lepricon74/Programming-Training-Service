import React from 'react';
import { connect } from 'react-redux';
import Learning from '../components/Learning/Learning';
import { navigateToPage, setLearningTextDefault } from '../actionCreators';

export default connect(
    (state,props) => ({

    }),
    (dispatch,props) =>({
        setLearningTextDefault: ()=> dispatch(setLearningTextDefault()),
        onNavigateToPage: value => dispatch(navigateToPage(value)),
    })
)(Learning)