import React from 'react';
import { connect } from 'react-redux';
import NavigationMenu from '../components/NavigationMenu/NavigationMenu';
import { navigateToPage } from '../actionCreators';

export default connect(
    (state,props) => ({
        auth: !state.userInformation.isAuthenticated
    }),
    (dispatch,props) =>({
        onNavigateToPage: value => dispatch(navigateToPage(value))
    })
)(NavigationMenu)