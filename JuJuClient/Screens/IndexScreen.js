import React, { useContext, useState, useEffect, useRef } from 'react';
import { StyleSheet, View, TouchableWithoutFeedback, TouchableOpacity, Image } from 'react-native';
import { Layout, Icon, Text, Button } from '@ui-kitten/components';
import { ThemeContext } from '../src/components/theme-context';
import SizedBox from '../src/components/SizedBox';
import Constants from 'expo-constants';

function IndexScreen({ navigation }) {

    const themeContext = React.useContext(ThemeContext);
    const themeIcon = (props) => (
      <Icon {...props} name={Constants.theme=='light' ? 'moon' : 'sun'}/>
    );

    return (
      <Layout style={styles.Layout}>
      <Image
      source={require('../assets/images/Banner.png')} 
      style={[styles.context, { flex: 3,justifyContent: 'flex-end', resizeMode: 'contain' }]}
      />
        <View style={[styles.context,{ flex: 2,justifyContent: 'flex-start', paddingTop:100}]}>
        <Text category='h4'>居居</Text>
        <Text category='h6'>大樓管理系統</Text>
        <SizedBox height={32} />
        <Button accessoryRight={themeIcon} onPress={themeContext.toggleTheme} appearance='ghost' status='basic' style={styles.themeBtn}></Button>
        <SizedBox height={32} />
        <TouchableOpacity onPress={() => { navigation.navigate('Login'); }} activeOpacity={0.1}>
          <Text category='h2'>開始使用</Text>
        </TouchableOpacity>
      </View>
      </Layout>
    );
  }

  const styles = StyleSheet.create({
    Layout: {
      flex: 1,
      alignItems: 'center',
      justifyContent: 'center',
    },
    context: {
    width:400,
    alignItems: 'center',
    },
    themeBtn: {
      width: 50,
      height: 50,
      borderRadius: 50,
    },
    themeIcon: {
      // width: 30,
      // height: 30,
    },
  });
  
    export default IndexScreen;
    