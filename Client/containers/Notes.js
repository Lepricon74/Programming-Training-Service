import React from 'react';
import { connect } from 'react-redux';
import Notes from '../components/Notes/Notes';
import { navigateToPage} from '../actionCreators';


export default connect(
    (state,props) => ({
        notesList: state.notesList
    }),
    (dispatch,props) =>({
        onNavigateToPage: value => dispatch(navigateToPage(value))

    })
)(Notes)