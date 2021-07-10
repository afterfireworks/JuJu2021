import React from 'react';
import axios from "../src/axios";
import {useContext, useState, useEffect} from 'react';
import {
  View,
  Text,
  Button,
  Dimensions,
  StyleSheet,
  Alert,
  TouchableOpacity,
} from 'react-native';
import {BarCodeScanner} from 'expo-barcode-scanner';
import Constants from 'expo-constants';
// import { RNCamera } from 'react-native-camera';
// import BarcodeMask from 'react-native-barcode-mask';

const finderWidth = 280;
const finderHeight = 230;
const width = Dimensions.get ('window').width;
const height = Dimensions.get ('window').height;
const viewMinX = (width - finderWidth) / 2;
const viewMinY = (height - finderHeight) / 2;

function ScannerScreen ({ navigation }) {
  const [hasPermission, setHasPermission] = useState (null);
  const [scanned, setScanned] = useState (false);
  const Recipient = Constants.userAccount;
  

  useEffect (() => {
    (async () => {
      const {status} = await BarCodeScanner.requestPermissionsAsync ();
      setHasPermission (status === 'granted');
    }) ();
  }, []);

  const handleBarCodeScanned = ({type, data}) => {
    setScanned (true);
    ScannerAlert(data); 

    function ScannerAlert(data){
    Alert.alert(
      '掃描成功',
      '要領取包裹嗎 ?',
      // `掃描類型 : ${type}`,
      // `連結 :  ${data}${Recipient}`,
      [
        {text: '暫不領取', onPress: () => console.log("Cancel Pressed"), style: 'cancel'},
        {text: '領取包裹', onPress: () => {
            axios
            .get(`${data}${Recipient}`)
            .then( (response) => {
              // console.log(response.data);
              if( response.data == true){
                alert('領取成功');
              }
              else {alert('請重新掃描一次');}
              
              })
            .catch( (error) => {alert('不符合領取資格');})
            .finally (() => {});
        }},
      ],
      { cancelable: true  }
    );
  };
    
  };

  if (hasPermission === null) {
    return <Text style={styles.container}>未開啟相機權限</Text>;
  }
  if (hasPermission === false) {
    return <Text style={styles.container}>未允許相機權限</Text>;
  }

  return (
    <View style={styles.container}>
      <BarCodeScanner
        onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
        style={StyleSheet.absoluteFillObject}
      />
      {scanned &&
        <Button 
          title={'再次掃描 ?'}
          onPress={() => setScanned (false)}
          style={styles.button}
        />}
    </View>
  );
}

const styles = StyleSheet.create ({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: '80%',
  },
  button: {
    alignItems: 'center',
    backgroundColor: 'rgb(0, 0, 0)',
    borderRadius: 8,
    height: 48,
    justifyContent: 'center',
  },
});

export default ScannerScreen;
