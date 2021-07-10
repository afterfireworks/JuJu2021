import 'react-native-gesture-handler';
import { StatusBar } from 'expo-status-bar';
import React from 'react';
// import { StyleSheet, Text, View, Image, TouchableHighlight, TextInput } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { IndexStack, Home, ScannerStack, FunctionStack } from './Screens';
import * as eva from '@eva-design/eva';
import { IconRegistry, ApplicationProvider, Layout, Text } from '@ui-kitten/components';
import { EvaIconsPack } from '@ui-kitten/eva-icons';
import { default as theme } from './src/components/theme.json';
import { ThemeContext } from './src/components/theme-context';
import Constants from 'expo-constants';

export default function App() {
  const [theme, setTheme] = React.useState('light');

  const toggleTheme = () => {
  const nextTheme = theme === 'light' ? 'dark' : 'light';
    setTheme(nextTheme);
    Constants.theme = theme;
  };
  return (
    <>
    <IconRegistry icons={EvaIconsPack} />
    <ThemeContext.Provider value={{ theme, toggleTheme }}>
    <ApplicationProvider {...eva} theme={eva[theme]}>
      <NavigationContainer>
      <IndexStack/>
      </NavigationContainer>
    </ApplicationProvider>
    </ThemeContext.Provider>
    </>
  );
}
