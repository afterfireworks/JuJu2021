import React from 'react';
import { View } from 'react-native';
const SizedBox = ({ height, width }) => {
    return React.createElement(View, { style: { height, width } });
};
export default SizedBox;
