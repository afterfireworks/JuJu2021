import React from 'react';
import axios from "../../src/axios";
import { useContext, useState, useEffect,} from "react";
import { StyleSheet, View, FlatList, SafeAreaView, TouchableOpacity } from 'react-native';
import { Layout, Card, Icon, Text, Input, Button, Spinner } from '@ui-kitten/components';
import Constants from 'expo-constants';
import styles from '../../src/components/Style-default'
import SizedBox from '../../src/components/SizedBox';


function MeetingScreen({ navigation }) {

  const [DATA, setDATA] = useState('');

  useEffect(() => {
    let repeat;
    async function axiosData() {
        await axios.get('Api_Meetings')
            .then(function (response) {
              // console.log(response.data);
              setDATA(response.data);
              // repeat = setTimeout(axiosData, 60000);
            });
    };
    axiosData();

    return () => {
        if (repeat) {
            clearTimeout(repeat);
        }
    }

}, []);


  return (
    <Layout style={styles.Layout}>
      <View style={styles.title}>
      <Text category='h4'>社區會議</Text>
      </View>
      <SafeAreaView style={styles.content}>
          <FlatList 
            contentContainerStyle={{margin:4}} 
            horizontal={false} 
            numColumns={1} 
            mode="small"
            data={DATA} 
            keyExtractor={item => String(item.SN)} 
            renderItem={({ item }) =>
            <Card style={styles.card, 
              {shadowOffset: {
                width: 0,
                height: 2,
                },
              shadowOpacity: 0.25,
              shadowRadius: 3.84,
              elevation: 5,}}
              status='basic'
              header={(props) => (
              <View {...props} style={[props.style, styles.headerContainer]}>
              <Text category='h6'>{item.Title}</Text>
              <Text>{item.Date.replace("T", ' ')}</Text>
              </View>
               )}
              footer={(props) => (
              <View {...props} style={[props.style, styles.footerContainer]}>
              <Button
                style={styles.footerControl} size='tiny' appearance='ghost' status='control'
                onPress={()=>{
                Constants.YutubeLink = item.URL.substr(17);
                navigation.navigate ('MeetingPlayer');
                //備用於導航至新畫面崩潰
                // navigationOptions: { animationEnabled: false }
              }}
              >
              <Text style={styles.buttonTitle}>觀看影片</Text>
              </Button>
              </View>
            )}>
            <Text>{item.URL}</Text>
            <SizedBox height={16} />
            </Card>
          }
          /> 
      </SafeAreaView>
    </Layout>
  );
}

const thisStyles = StyleSheet.create({
  card: {
    flex: 1,
    marginHorizontal: 30,
  },
  headerContainer: {
    paddingHorizontal: 15,
  },
  footerContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
  },
  footerControl: {
    marginHorizontal: 2,
  },
});


export default MeetingScreen;