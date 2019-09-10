import React from 'react';
import { ScrollView, StyleSheet , Alert } from 'react-native';
import InventoryItem from './InventoryItem';
import Swiper from 'react-native-swiper';

export default class TestScreen extends React.Component {
  static navigationOptions = {
    title: 'Inventory',
    // headerStyle: {
    //   backgroundColor: 'rgb(139, 195, 74)',
    // },
    // headerTintColor: 'rgba(0, 0, 0, 0.87)',
    // headerTitleStyle: {
    //   fontWeight: 'bold',
    // },
  };

  constructor(props) {
    super(props);
    this.state = {  items:[]
        }
      }

        componentDidMount(){
          
          return fetch('http://192.168.1.156/Mainframe.BuyerSupplier.Api/api/values/', {
            method: 'GET',
            headers: {
              Accept: 'application/json',
              'Content-Type': 'application/json',
            }
          }).then((response) => response.json())
            .then((responseJson) => {
      
              this.setState({
               
                items: responseJson,
              }, function(){
      
              });
      
            })
            .catch((error) =>{
              console.error(error);
              
              Alert.alert(
                'Error',
                'Error occurred while communicating with the API',
                [ 
                  {text: 'OK', onPress: () => console.log('OK Pressed')}
                ],
                { cancelable: false }
              );
            });
        }

        addInventory = item => {
          Alert.alert(
            'Alert Title',
            'Qty : '+ item.qty + ' Unit Price'+item.unitPrice,
            [
              {text: 'Ask me later', onPress: () => console.log('Ask me later pressed')},
              {text: 'Cancel', onPress: () => console.log('Cancel Pressed'), style: 'cancel'},
              {text: 'OK', onPress: () => console.log('OK Pressed')},
            ],
            { cancelable: false }
          )
          };

        renderInventoryItem = item =>
        <InventoryItem key={item.id} item={item} onClick={this.addInventory} />;

  render() {
    return (
      <ScrollView style={styles.container}>
      
        <Swiper style={styles.slideContainer} showsButtons={false} loop={false} showsPagination={false}>
        {
          this.state.items.map(this.renderInventoryItem)     
        }            
        </Swiper>
      </ScrollView>
    );
  }
}

const styles = StyleSheet.create({
    slideContainer: {
      height: 500,
    },
    slide: {
      padding: 15,
      height: 500,
    },
    slide1: {
      backgroundColor: '#FEA900',
    },
    slide2: {
      backgroundColor: '#B3DC4A',
    },
    slide3: {
      backgroundColor: '#6AC0FF',
    },
    text: {
      fontSize: 36,
      padding:10
    },  welcomeContainer: {
        alignItems: 'center',
        marginTop: 10,
        marginBottom: 20,
      },
      welcomeImage: {
        width: 200,
        height:160,
        resizeMode: 'contain',
        marginTop: 3,
        marginLeft: -10,
      }
  });
