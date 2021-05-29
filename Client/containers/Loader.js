import React from 'react';
import { connect } from 'react-redux';
import Loader from '../components/Loader/Loader';

export default connect(
    (state, props) => ({
        fontColor: props.fontColor
    }),
    (dispatch, props) => ({
    })
)(Loader);