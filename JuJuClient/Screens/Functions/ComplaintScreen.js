import React from 'react';
import axios from "../../src/axios";
import { useContext, useState, useEffect, useRef } from "react";
import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  Pressable,
  SafeAreaView,
  StyleSheet,
  // Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { Layout, Icon, Text, Input, Button, Spinner } from '@ui-kitten/components';
import {Controller, useForm} from 'react-hook-form';
import Constants from 'expo-constants';
import styles from '../../src/components/Style-default'
import SizedBox from '../../src/components/SizedBox';


function ComplaintScreen({ navigation }) {
 const submitIcon = (props) => (
      <Icon {...props} name={'done-all'}/>
  );
 
  const circleIcon = (props) => (
     <Icon
        style={styles.icon}
        fill='#8F9BB3'
        name='alert-circle'
      />
  );

  const titleInput = React.useRef (null);
  const descriptionInput = React.useRef (null);
  const {
    control,
    handleSubmit,
    register,
    setValue,
    reset,
    formState: {errors},
  } = useForm ({
    defaultValues: {
      Title: '',
      Description: '',
    },
  });
  const onChange = arg => {
    return {
      value: arg.nativeEvent.text,
    };
  };

  const CheckAlert=()=>{
    Alert.alert (
        '上傳失敗',
        '請輸入事由與詳細敘述',
        [
          {text: '重新輸入', onPress: () => console.log ('OK Pressed')},
        ],
        {cancelable: false}
      );
  };

  const SuccessAlert=()=>{
    Alert.alert(
      '上傳成功',
      '請等待主委處理',
      [
        {text: '返回功能頁', onPress: () => {navigation.navigate('Function')}},
        // {text: 'No', onPress: () => console.log('No button clicked'), style: 'cancel'},
      ],
      { cancelable: false }
    );
  };

  const onSubmit = handleSubmit (({Title, Description}) => {
    if (Title != '' && Description != '') {
      var now = new Date(); 
      var date = now.getFullYear() + "/" + parseInt(now.getMonth() + 1)  + "/" + now.getDate() + " " + now.getHours() + ":" + now.getMinutes();

      axiosData (date, Title, Description);

    } else {
      CheckAlert();
    }

    async function axiosData () {
    
      await axios
            .post('Complaints',{
            Account: 'noise',
            Date: date,
            Title: Title,
            Description: Description,
            Solved: false,
            })
            .then( (response) => {
              console.log(response);
              SuccessAlert();
              })
            .catch( (error) => console.log(error))
            .finally (() => {});
    }
  });

  useEffect(() => {
  }, []);

  return (
      <Layout style={styles.Layout}>
      <View style={styles.title}>
      <Text category='h4'>抱怨申訴</Text>
      </View>
      <SafeAreaView style={styles.content}>
      <KeyboardAvoidingView
          behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
          style={styles.container}
      >
      <SizedBox height={32} />
      <Text>事由</Text>
      <SizedBox height={16} />
      <Pressable>
            <View style={styles.form}>
              <Controller
                control={control}
                name="Title"
                render={({field: {onChange, onBlur, value}}) => (
                  <Input
                    style={styles.input}
                    maxLength={20}
                    textAlign="center"
                    placeholder="請輸入事由"
                    ref={titleInput}
                    autoCapitalize="none"
                    // autoCompleteType="username"
                    // textContentType="username"
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
      <SizedBox height={32} />
      <Text>詳細描述</Text>
      <SizedBox height={16} />
      <Pressable>
            <View style={styles.form}>
              <Controller
                control={control}
                name="Description"
                render={({field: {onChange, onBlur, value}}) => (
                  <Input
                    style={styles.input}
                    maxLength={20}
                    textAlign="center"
                    placeholder="請輸入詳細描述"
                    ref={descriptionInput}
                    multiline = {true}
                    numberOfLines = {10}
                    autoCapitalize="none"
                    // autoCompleteType="username"
                    // textContentType="username"
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

      <SizedBox height={32} />

          {/* <TouchableOpacity 
          onPress={onSubmit}
          >
            <View style={styles.button}>
              <Text style={styles.buttonTitle}>送出</Text>
            </View>
          </TouchableOpacity> */}

          <Button accessoryRight={submitIcon} appearance='ghost' onPress={onSubmit}>
            <Text category='h6'>送出</Text>
          </Button>
 
        </KeyboardAvoidingView>
      </SafeAreaView>
      </Layout>
  );

}

const thisStyles = StyleSheet.create({
   form: {
    flexDirection: 'row',
    alignItems: 'center',
    // backgroundColor: 'rgb(58, 58, 60)',
    borderRadius: 8,
    paddingHorizontal: 16,
  },
  button: {
    alignItems: 'center',
    borderRadius: 8,
    height: 48,
    justifyContent: 'center',
  },
  input: {
    width:300,
  },
  icon: {
    width: 32,
    height: 32,
  },
});

export default ComplaintScreen;