import React from 'react';
import axios from '../src/axios';
import { useContext, useState, useEffect, useRef } from 'react';
import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  Pressable,
  SafeAreaView,
  StyleSheet,
  // Text,
  TextInput,
  TouchableWithoutFeedback,
  TouchableOpacity,
  View,
} from 'react-native';
import { Layout, Icon, Text, Input, Button, Spinner } from '@ui-kitten/components';
import SizedBox from '../src/components/SizedBox';
import { Controller, useForm } from 'react-hook-form';
import Constants from 'expo-constants';

// const AlertIcon = (props) => (
//   <Icon {...props} name='alert-circle-outline'/>
// );

function LoginScreen({navigation}) {
  

  const [secureTextEntry, setSecureTextEntry] = React.useState(true);

  const toggleSecureEntry = () => {
    setSecureTextEntry(!secureTextEntry);
  };

  const accIcon = (props) => (
      <Icon {...props} name={'people'}/>
  );
  
  const pwdIcon = (props) => (
    <TouchableWithoutFeedback onPress={toggleSecureEntry}>
      <Icon {...props} name={secureTextEntry ? 'eye-off' : 'eye'}/>
    </TouchableWithoutFeedback>
  );

  const LoadingIndicator = (props) => (
  <View style={[props.style, styles.indicator]}>
    <Spinner size='small'/>
  </View>
  );

  const accountInput = React.useRef (null);
  const passwordInput = React.useRef (null);
  const {
    control,
    handleSubmit,
    register,
    setValue,
    reset,
    formState: {errors},
  } = useForm ({
    defaultValues: {
      Account: '',
      Password: '',
    },
  });

  const onChange = arg => {
    return {
      value: arg.nativeEvent.text,
    };
  };

  const onSubmit = handleSubmit (({Account, Password}) => {
    if (Account != '' && Password != '') {
      axiosData (Account, Password);
    } else {
      alert ('請輸入帳號密碼');
      Alert.alert (
        '請輸入帳號密碼',
        [
          {
            text: 'Cancel',
            onPress: () => console.log ('Cancel Pressed'),
            style: 'cancel',
          },
          {text: 'OK', onPress: () => console.log ('OK Pressed')},
        ],
        {cancelable: false}
      );
    }

    async function axiosData (Account, Password) {
      await axios
        .get ('Api_UserLogin?LoginAccount=' + Account + '&LoginPassword=' + Password)
        .then (function (response) {
          if (response.data[0].isLogin == 1) {
            // console.log(response.data[0])
            Constants.isLogin = response.data[0].isLogin;
            Constants.userAccount = response.data[0].userAccount;
            Constants.userName = response.data[0].userName;
            navigation.navigate ('Home');
          } else {
            alert (`${response.data[0].errorMessages}`);
            Alert.alert (
              `${response.data[0].errorMessages}`,
              '帳號密碼有誤',
              [
                {
                  text: 'Cancel',
                  onPress: () => console.log ('Cancel Pressed'),
                  style: 'cancel',
                },
                {text: 'OK', onPress: () => console.log ('OK Pressed')},
              ],
              {cancelable: false}
            );
          }
        })
        .catch (error => {
          console.error (error);
        })
        .finally (() => {});
    }
  });

  return (
    <Layout style={styles.Layout}>
      <SafeAreaView>
        <KeyboardAvoidingView
          behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
          style={styles.content}
        >
          <View style={styles.title}>
            <Text category='h3'>系統登入</Text>
          </View>

          <SizedBox height={32} />
          
          <Pressable>
            <View style={styles.form}>
              <Controller
                control={control}
                name="Account"
                render={({field: {onChange, onBlur, value}}) => (
                  <Input 
                    style={styles.input}
                    maxLength={20}
                    textAlign="center"
                    placeholder="帳號"
                    accessoryRight={accIcon}
                    ref={accountInput}
                    autoCapitalize="none"
                    autoCompleteType="username"
                    textContentType="username"
                    autoCorrect={false}
                    returnKeyType="next"
                    onBlur={onBlur}
                    onChangeText={value => onChange (value)}
                    value={value}
                  />
                )}
              />
            </View>
          </Pressable>

          <SizedBox height={16} />

          <Pressable>
            <View style={styles.form}>
              <Controller
                control={control}
                name="Password"
                render={({field: {onChange, onBlur, value}}) => (
                  <Input 
                    style={styles.input}
                    maxLength={20}
                    textAlign="center"
                    placeholder="密碼"
                    // caption={renderCaption}
                    secureTextEntry={secureTextEntry}
                    accessoryRight={pwdIcon}
                    ref={passwordInput}
                    autoCapitalize="none"
                    autoCompleteType="password"
                    textContentType="password"
                    autoCorrect={false}
                    returnKeyType="done"
                    onBlur={onBlur}
                    onChangeText={value => onChange (value)}
                    value={value}
                  />
                )}
              />
            </View>
          </Pressable>

          <SizedBox height={32} />

          {/* <TouchableOpacity  onPress={onSubmit}>
            <View style={styles.button}>
              <Text style={styles.buttonTitle}>登入</Text>
            </View>
          </TouchableOpacity> */}
          
          <Button style={styles.button} appearance='ghost' accessoryLeft={LoadingIndicator} onPress={onSubmit}>
            <Text category='h6'>登入</Text>
          </Button>
          

        </KeyboardAvoidingView>
      </SafeAreaView>
    </Layout>
  );
}

const styles = StyleSheet.create ({
  Layout: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  content: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  button: {
    alignItems: 'center',
    // backgroundColor: 'rgb(0, 0, 0)',
    borderRadius: 8,
    height: 48,
    justifyContent: 'center',
  },
  buttonTitle: {
    // color: '#FFFFFF',
    fontSize: 17,
    fontWeight: '600',
    lineHeight: 22,
  },
  content: {
    flex: 1,
    justifyContent: 'center',
    paddingHorizontal: 16,
    paddingVertical: 32,
  },
  forgotPasswordContainer: {
    alignItems: 'flex-end',
  },
  form: {
    alignItems: 'center',
    // flexDirection: 'row',
    width: 250,
    height: 48,
    paddingHorizontal: 16,
  },
  label: {
    // color: 'rgba(235, 235, 245, 0.6)',
    fontSize: 15,
    fontWeight: '400',
    lineHeight: 20,
    width: 80,
  },
  safeAreaView: {
    flex: 1,
  },
  subtitle: {
    // color: 'rgba(235, 235, 245, 0.6)',
    fontSize: 17,
    fontWeight: '400',
    lineHeight: 22,
  },
  textButton: {
    // color: '#FFFFFF',
    fontSize: 15,
    fontWeight: '400',
    lineHeight: 20,
  },
  input: {
    // color: '#FFFFFF',
    flex: 1,
  },
  title: {
    height: 100,
    alignItems: 'center',
    justifyContent: 'center',
  },
});

export default LoginScreen;
