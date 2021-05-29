import React, { useState, useRef, useEffect } from "react";
import PropTypes from 'prop-types';
import './style.css';

export default function Loader({ fontColor }) {
    return (<div id='loaderForm'><p style={{ 'color': fontColor }}>Данные еще загружаются. Пожалуйста, подождите</p> <div id='loader' /> </div>);
}

Loader.propTypes = {
    fontColor: PropTypes.string
};