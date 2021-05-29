import React from 'react';
import { connect } from 'react-redux';
import NotFound from '../components/NotFound/NotFound';
import { navigateToPage } from '../actionCreators';

export default connect(
    (state,props) => ({

    }),
    (dispatch,props) =>({
        onNavigateToPage: value => dispatch(navigateToPage(value))
    })
)(NotFound)