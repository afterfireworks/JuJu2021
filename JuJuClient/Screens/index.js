import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { getFocusedRouteNameFromRoute } from '@react-navigation/native' ;  
import { StyleSheet, Text, View, Image, TouchableHighlight, TextInput } from 'react-native';

import IndexScreen from './IndexScreen';
import LoginScreen from './LoginScreen';
import HomeScreen from './HomeScreen';
import ScannerScreen from './ScannerScreen';
import FunctionScreen from './FunctionScreen';
import UserScreen from './Functions/UserScreen';
import CollectorScreen from './Functions/CollectorScreen';
import PackageScreen from './Functions/PackageScreen';
import ReturnOfGoodsScreen from './Functions/ReturnOfGoodsScreen';
import MeetingScreen from './Functions/MeetingScreen';
import MeetingPlayerScreen from './Functions/MeetingPlayerScreen';
import ComplaintScreen from './Functions/ComplaintScreen';

// export const TabNav = TabNavigator({
// });

export const Tab = createBottomTabNavigator();
export const Home = () => {
  return (
      <Tab.Navigator

      screenOptions={({ route }) => ({
        tabBarIcon: ({ focused, color, size }) => { 
          //focused為連結到該頁面的意思
          let iconPath;
          let iconSize;
          let iconTop;
          if (route.name === 'Home') {
            iconPath = focused
              ? require('../assets/icons/home.png') :
              require('../assets/icons/home.png');
            iconSize=focused?30:18;
            iconTop=focused?0:7.5;
          } else if (route.name === 'Scanner') {
            iconPath = focused
              ? require('../assets/icons/scanner.png') :
              require('../assets/icons/scanner.png');
            iconSize=focused?30:18;
            iconTop=focused?0:7.5;
          } else if (route.name == 'Function') {
            iconPath = focused
              ? require('../assets/icons/function.png') :
              require('../assets/icons/function.png');
              iconSize=focused?30:18;
              iconTop=focused?0:7.5;
          }
          return (
            <Image
              style={{ width: iconSize, height: iconSize, top:iconTop ,zIndex:10}}
              source={iconPath}
            />
          );
        },
        tabBarLabel: ({ focused, color}) => {
          let showFont;
          if (route.name === 'Home') {
            showFont= focused? 'none' :'flex';
          } else if (route.name === 'Scanner') {
            showFont= focused? 'none' :'flex';
          } else if (route.name == 'Function') {
            showFont= focused? 'none' :'flex';
            }
          return (
            <Text style={{ fontSize: 12,marginTop: 9,marginBottom: 7,padding: 0,display:showFont,color:'#707070'}} >
            {route.name}
            </Text>
          );
        },
      })}

      tabBarOptions={{
        activeTintColor: '#000',
        inactiveTintColor: '#707070',
        labelStyle: {
          fontSize: 12,
          marginTop: 5,
          marginBottom: 7,
          padding: 0,
        },
        style: {
          // position: 'absolute',
          shadowColor: "#000",
          shadowOffset: { width: 0, height: -3 },
          shadowOpacity: .1,
        },
      }}

      >
        <Tab.Screen name="Home" component={HomeScreen}
          options={props => {
            return {
              title: "公告",
            };
          }}
        />
        <Tab.Screen name="Scanner" component={ScannerStack} 
        options={props => {
          return {
            title: "掃描器",
            // tabBarVisible: !props.route.state || props.route.state.index === 0,
          };
        }}
        />
        <Tab.Screen name="Function" component={FunctionStack} 
        options={props => {
          return {
            title: "集裝箱",
            // tabBarVisible: !props.route.state || props.route.state.index === 0,
          };
        }}
        />
      </Tab.Navigator>
  );
}

export const Stack = createStackNavigator();

export const IndexStack = () => {
  return (
    <Stack.Navigator
     screenOptions={{
     headerShown: false,
     title: "公告"
  }}>
      <Stack.Screen name="Index" component={IndexScreen} />
      <Stack.Screen name="Login" component={LoginScreen} />
      <Stack.Screen name="Home" component={Home} />
    </Stack.Navigator>
  );
}

export const ScannerStack = () => {
  return (
    <Stack.Navigator
    screenOptions={{
      headerShown: false,
     title: "掃描器"
  }}>
      <Stack.Screen name="Scanner" component={ScannerScreen} />
      {/* <Stack.Screen name="Edit" component={EditScreen}/>   */}
    </Stack.Navigator>
  );
}

export const FunctionStack = () => {
  return (
    <Stack.Navigator
     screenOptions={{
      //  headerShown: false,
     title: "集裝箱"
  }}>
      <Stack.Screen name="Function" component={FunctionScreen} options={{headerShown:false}}/>
      <Stack.Screen name="User" component={UserScreen} />
      <Stack.Screen name="Collector" component={CollectorScreen} />
      <Stack.Screen name="Package" component={PackageScreen} />
      <Stack.Screen name="ReturnOfGoods" component={ReturnOfGoodsScreen} />
      <Stack.Screen name="Meeting" component={MeetingStack} />
      <Stack.Screen name="Complaint" component={ComplaintScreen} />
    </Stack.Navigator>
  );
}

export const MeetingStack = () => {
  return (
    <Stack.Navigator
    screenOptions={{
      headerShown: false,
     title: "會議記錄"
  }}>
      <Stack.Screen name="Meeting" component={MeetingScreen} />
      <Stack.Screen name="MeetingPlayer" component={MeetingPlayerScreen} />
    </Stack.Navigator>
  );
}
