import React from 'react';
import { connect } from 'react-redux';
import Authors from '../components/Authors/Authors';
import { navigateToPage } from '../actionCreators';


export default connect(
    (state,props) => ({
    }),
    (dispatch,props) =>({
        onNavigateToPage: value => dispatch(navigateToPage(value))
    })
)(Authors)