import React from 'react';
import { connect } from 'react-redux';
import TestsNavigation from '../components/TestsNavigation/TestsNavigation';
import { navigateToPage, requestTest, setTest, setTestResult} from '../actionCreators';


export default connect(
    (state,props) => ({
        testsList : state.testsList,
        page: state.page,
    }),
    (dispatch,props) =>({
        onNavigateToPage: value => dispatch(navigateToPage(value)),
        requestTest: () => dispatch(requestTest()),
        setTest: value => dispatch(setTest(value)),
        setTestResult: value => dispatch(setTestResult(value)),
    })
)(TestsNavigation)