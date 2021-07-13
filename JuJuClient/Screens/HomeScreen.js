import React from 'react';
import axios from "../src/axios";
import { useContext, useState, useEffect } from "react";
import { StyleSheet, View, FlatList, SafeAreaView, TouchableOpacity } from 'react-native';
import { Layout, Card, Text, Button } from '@ui-kitten/components';
import styles from '../src/components/Style-default'
import SizedBox from '../src/components/SizedBox';

// {navigation}
function HomeScreen({ navigation }) {
  
  const [DATA, setDATA] = useState('');

  useEffect(() => {
    let repeat;
    async function axiosData() {
      await axios.get('Api_Announcements')
        .then(function (response) {
          setDATA(response.data);
          // console.log(response.data);
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

  const Header = (props) => (
  <View {...props}>
    <Text category='h6'>{item.Title}</Text>
    <Text category='s1'>{item.Date.replace("T", ' ')}</Text>
  </View>
);

const Footer = (props) => (
  <View {...props} style={[props.style, styles.footerContainer]}>
    <Button
      style={styles.footerControl}
      size='small'
      status='basic'>
      CANCEL
    </Button>
    <Button
      style={styles.footerControl}
      size='small'>
      ACCEPT
    </Button>
  </View>
);

  return (
    <Layout style={styles.Layout}>
      <View style={styles.title}>
      <Text category='h4'>最新公告</Text>
      </View>
      <SafeAreaView style={styles.content}>
          <FlatList 
            // contentContainerStyle={{margin:10}} 
            horizontal={false} 
            numColumns={1} 
            mode="small"
            data={DATA} 
            keyExtractor={item => String(item.SN)} 
            renderItem={({ item }) =>

            <Card style={styles.card}
              status='basic'
              header={(props) => (
              <View {...props} style={[props.style, styles.headerContainer]}>
              <Text category='h6'>{item.Title}</Text>
              <Text>{item.Date.replace("T", ' ')}</Text>
              </View>
               )}
              footer={(props) => (
              <View {...props} style={[props.style, styles.footerContainer]}>
              {/* item.Picture */}
              {/* <Button style={styles.footerControl} size='tiny' appearance='ghost' status='info'>附件</Button> */}
              </View>
            )}>
            <Text>{item.Description}</Text>
            </Card>

          }/> 
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


export default HomeScreen;