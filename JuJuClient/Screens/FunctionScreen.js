import React from 'react';
import axios from "../src/axios";
import { StyleSheet, View, TouchableOpacity } from 'react-native';
import { Layout, Icon, Text, Input, Button, Spinner } from '@ui-kitten/components';
import styles from '../src/components/Style-default'
import SizedBox from '../src/components/SizedBox';
import Constants from 'expo-constants';

function FunctionScreen({navigation}) {

//  const [DATA, setDATA] = useState('');

//   useEffect(() => {
//     let repeat;
//     async function axiosData() {
//       await axios.get('Announcements')
//         .then(function (response) {
//           setDATA(response.data);
//           console.log(response.data);
//           repeat = setTimeout(axiosData, 60000);
//         });
//     };
//     axiosData();

//     return () => {
//       if (repeat) {
//         clearTimeout(repeat);
//       }
//     }
//   }, []);

  return (
    <Layout style={styles.Layout}>
      <Text category='h4'>功能頁面</Text>
      <SizedBox height={40} />
      <TouchableOpacity onPress={() => { navigation.navigate('User'); }} style={styles.Btn} activeOpacity={0.6}>
        <Text>個人資料</Text>
      </TouchableOpacity>
      <SizedBox height={25} />
      <TouchableOpacity onPress={() => { navigation.navigate('Collector'); }} style={styles.Btn} activeOpacity={0.6}>
        <Text>代收人</Text>
      </TouchableOpacity>
      <SizedBox height={25} />
      <TouchableOpacity onPress={() => { navigation.navigate('Package'); }} style={styles.Btn} activeOpacity={0.6}>
        <Text>包裹</Text>
      </TouchableOpacity>
      <SizedBox height={25} />
      <TouchableOpacity onPress={() => { navigation.navigate('ReturnOfGoods'); }} style={styles.Btn} activeOpacity={0.6}>
        <Text>退貨</Text>
      </TouchableOpacity>
      <SizedBox height={25} />
      <TouchableOpacity onPress={() => { navigation.navigate('Meeting'); }} style={styles.Btn} activeOpacity={0.6}>
        <Text>社區會議</Text>
      </TouchableOpacity>
      <SizedBox height={25} />
      <TouchableOpacity onPress={() => { navigation.navigate('Complaint'); }} style={styles.Btn} activeOpacity={0.6}>
        <Text>抱怨申訴</Text>
      </TouchableOpacity>
      <SizedBox height={60} />
      <TouchableOpacity style={styles.Btn} activeOpacity={0.6}
        onPress={() => { 
        Constants.userAccount = '';
        navigation.navigate('Login'); 
        }} 
        >
        <Text>帳號登出</Text>
      </TouchableOpacity>
    </Layout>
  );
}

const thisStyles = StyleSheet.create({
  Btn: {

  }
});

export default FunctionScreen;