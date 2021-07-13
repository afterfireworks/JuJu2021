import React from 'react';
import axios from "../../src/axios";
import { useContext, useState, useEffect } from "react";
import { StyleSheet, View, FlatList, SafeAreaView, TouchableHighlight } from 'react-native';
import { Layout, Card, Icon, Text, Input, Button, Spinner } from '@ui-kitten/components';
import Constants from 'expo-constants';
import styles from '../../src/components/Style-default'
import SizedBox from '../../src/components/SizedBox';


function PackageScreen({ navigation }) {

    const [DATA, setDATA] = useState('');

    useEffect(() => {
        let repeat;
        async function axiosData() {
            await axios.get(`Api_Packages?userAccount=${Constants.userAccount}`)
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


    return (
      <Layout style={styles.Layout}>
      <View style={styles.title}>
      <Text category='h4'>包裹</Text>
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
                <Text>配送日 : {item.ArrivalDate.replace("T", ' ')}</Text>
                </View>
                 )}
              >
            <Text>領取狀態 : {item.Sign}</Text>     
            <Text>領取日期 : {item.SignDate==null?"未領取":item.SignDate.replace("T", ' ')}</Text>
            <Text>領取人 : {item.Signer==null?"未領取":item.Signer}</Text>
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

export default PackageScreen;