import React from 'react';
import { connect } from 'react-redux';
import LearningArea from '../components/LearningArea/LearningArea';


export default connect(
    (state,props) => ({
        learningArea: state.learningArea,
        pageIndication: props.pageIndication
    }),
    (dispatch,props) =>({

    })
)(LearningArea)