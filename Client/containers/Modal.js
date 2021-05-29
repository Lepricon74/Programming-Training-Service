import React from 'react';
import { connect } from 'react-redux';
import Modal from '../components/Modal/Modal';
import { requestNotes, setNotes} from '../actionCreators';


export default connect(
    (state,props) => ({
        page: state.page,
    }),
    (dispatch,props) =>({
        requestNotes: ()=> dispatch(requestNotes()),
        setNotes: (value) => dispatch(setNotes(value))

    })
)(Modal)