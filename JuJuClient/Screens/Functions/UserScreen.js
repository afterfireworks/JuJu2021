import React from 'react';
import axios from "../../src/axios";
import { useContext, useState, useEffect } from "react";
import { StyleSheet, View, Image, TouchableHighlight } from 'react-native';
import { Layout, Text, Input, Button, Spinner } from '@ui-kitten/components';
import { Avatar } from "react-native-elements";
import Constants from 'expo-constants';
import styles from '../../src/components/Style-default'
import SizedBox from '../../src/components/SizedBox';

function UserScreen({ navigation }) {

  const [userbase64Img, setuserbase64Img] = useState('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAABmJLR0QA/wD/AP+gvaeTAAABIElEQVRIie3WsWoCQRDG8b+mSNQUUUihtT6Epa8Q3yKkD2gb8hjprLW0EmzExlQRG1NKCg0kIEklprg9EMnN7ZxzYuEHU+3u/BZub1k450RzDbSBV2DtagK03FgqqQHvwDai5kDVGs0DMwENawpcWcIPHmhY95bwUAEPLOFPBbzyaZj1hAuKTXqdbl/4QwEvLOGZAvaa6wv3FHBXMTc2JeCb+IP1BRQtYQiuyjj40RqF4EYaC+gIuEwDBqgIcFnTKJMA31r08j3V5tHCTWHs7pCN/JcLoAF0gA3R33jj5jTcmsSpAy/AUsCiaunW1jXgLdBPgEVV3/UUU0R+3iStOXAjwc8poGE9SfA0RfhtF9r/6X+AnLSzA/JL8Gg857j5A5xV0rMCBL7kAAAAAElFTkSuQmCC');
  // const [userPhoto, setuserPhoto] = useState('');
  // const [userAvatar, setuserAvatar] = useState('');
  const [Account, setAccount] = useState('');
  const [Name, setName] = useState('');
  const [UserID, setUserID] = useState('');
  const [Address, setAddress] = useState('');
  const [Tel, setTel] = useState('');
  // const [Package, setPackage] = useState('');

  useEffect(() => {
    let repeat;
    let data;
    (async function axiosData() {
      await axios.get(`Residents?userAccount=${Constants.userAccount}`)
      // await axios.get(`Residents?userAccount=noise`)
        .then(function (response) {
          // console.log(response.data);
          data = response.data[0];
          // repeat = setTimeout(axiosData,1000);
          setAccount(data.Account);
          setName(data.Name);
          setUserID(data.ID);
          setAddress(data.Address);
          setTel(data.Tel);
          setuserbase64Img("data:image/png;base64,"+data.Photo)
          // setuserPhoto(<Image style={{width: 150, height: 150, resizeMode: 'contain'}} source={{uri: base64Img}} />);
          // setuserAvatar(
          //   <Avatar
          //   source={{uri: userbase64Img}}
          //   size="xlarge"
          //   title="LW" />
          //   );
        })
    })();

    return () => {
      if (repeat) {
        clearTimeout(repeat);
      }
    }

  }, []);

  return (
    <Layout style={ styles.Layout }>
      <View style={ styles.title }>
      <Text category='h4'>個人資料</Text>
      </View>
      {/* {userPhoto} */}
      {/* {userAvatar} */}
      <View style={styles.content}>
      <Avatar
            source={{uri: userbase64Img}}
            size="xlarge"
            title="LW" />
      <SizedBox height={32} />
      <Text>帳號</Text>
      <SizedBox height={10} />
      <Text category='h6'>{Account}</Text>
      <SizedBox height={10} />
      <Text>姓名</Text>
      <SizedBox height={10} />
      <Text category='h6'>{Name}</Text>
      <SizedBox height={10} />
      {/* <Text>身分證號碼 : {UserID}</Text> */}
      <Text>聯絡電話</Text>
      <SizedBox height={10} />
      <Text category='h6'>{Tel}</Text>
      <SizedBox height={10} />
      <Text>聯絡地址</Text>
      <SizedBox height={10} />
      <Text category='h6'>{Address}</Text>
      </View>
    </Layout>
  );
}

const thisStyles = StyleSheet.create({
    thisContent: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'flex-start',
    }
});

export default UserScreen;