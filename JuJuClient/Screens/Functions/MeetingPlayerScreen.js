import React from 'react';
import axios from "../../src/axios";
import { useState, useCallback, useRef} from "react";
import { Alert, StyleSheet, View, FlatList, SafeAreaView, TouchableHighlight} from 'react-native';
import { Layout, Text, Button } from '@ui-kitten/components';
import Constants from 'expo-constants';
import styles from '../../src/components/Style-default'
import SizedBox from '../../src/components/SizedBox';
import YoutubePlayer from 'react-native-youtube-iframe';

function MeetingPlayerScreen({ navigation }) {
  const [ playing , setPlaying ] = useState ( false ) ;  
  const onStateChange = useCallback((state) => {
    if (state === "ended") {
      setPlaying(false);
      Alert.alert("video has finished playing!");
    }
  }, []);
  const togglePlaying = useCallback(() => {
    setPlaying((prev) => !prev);
  }, []);

  return (
        <Layout
        //  renderToHardwareTextureAndroid={true}
        >
           <YoutubePlayer 
           webViewStyle={{opacity: 0.99, overflow: 'hidden', marginTop : 200 }}
           allowsFullscreenVideo = 'true'
           videoId={Constants.YutubeLink}
           height={750}
           play={playing}
           onChangeState={onStateChange}
          //  webViewProps = {
          //   {
          //    androidLayerType:  Platform.OS==="android" && Platform.Version<=22 ? "hardware" : "none" 
          //   }
          //  }
           />
           {/* <Button title={playing ? "pause" : "play"} onPress={togglePlaying} /> */}
        </Layout>
  );
}

const thisStyles = StyleSheet.create({
  // container: {
  //   flex: 1,
  //   backgroundColor: '#000',
  //   alignItems: 'center',
  //   justifyContent: 'center',
  // },
  // center: {
  //   alignItems: 'center',
  //   justifyContent: 'center',
  // },
});


export default MeetingPlayerScreen;