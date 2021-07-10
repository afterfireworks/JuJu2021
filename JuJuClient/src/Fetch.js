import React from 'react';
// import { FlatList, ActivityIndicator, Text, View  } from 'react-native';

export default class FetchToApi extends React.Component {

  constructor(props){
    super(props);
    this.state ={ isLoading: true}
  }

  componentDidMount(){
    return fetch('http://localhost:12346/api/'+props)
      .then((response) => response.json())
      .then((responseJson) => {

        this.setState({
          isLoading: false,
          dataSource: responseJson,
        }, function(){

        });

      })
      .catch((error) =>{
        console.log("ERRORAZO " , error);
      });
  }

  render(){
    console.log(this.state.dataSource);
    
    return(
      <View style={{flex: 1, paddingTop:20}}>
       
      </View>
    );
  }
}
