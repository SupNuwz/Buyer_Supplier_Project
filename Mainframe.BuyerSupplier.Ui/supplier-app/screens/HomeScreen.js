import React from 'react';
import {
  Image,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  View,
  Alert
} from 'react-native';

import Swipeout from 'react-native-swipeout';

import Touchable from 'react-native-platform-touchable';


var swipeoutBtns = [
  {
    text: 'ACCEPT', backgroundColor:"rgb(139, 195, 74)", width:100,color:"black",onPress:function _onAccept(item) {
      Alert.alert(
        'Alert Title',
        item,
        [
          {text: 'OK', onPress: () => console.log('OK Pressed')},
        ],
        { cancelable: false }
      )}
      }]



export default class HomeScreen extends React.Component {
  static navigationOptions = {
    title: 'Home',
  };

  constructor(props) {
    super(props);
    this.state = {  items:["Order #1001", "Order #1003","Order #1006","Order #1007","Order #1009","Order #10011","Order #10022",]
        }
      }

  renderoOrderItem = item =>
  <Swipeout right={swipeoutBtns} autoClose ={true}>
  <Touchable
          style={styles.option}
          background={Touchable.Ripple('#ccc', false)}
          >
          <View style={{ flexDirection: 'row' }}>

            <View style={styles.optionTextContainer}>
              <Text style={styles.optionText}>
                {item}
              </Text>
            </View>
          </View>
    </Touchable>
    </Swipeout>;

  render() {
    return (
      <View style={styles.container}>

        <Text style={styles.optionsTitleText}>
          Orders
        </Text>
        <ScrollView style={styles.scrollStyle}>
        <View>
        {
          this.state.items.map(this.renderoOrderItem)     
        }  
      </View>
   
        </ScrollView>

        <View style={styles.tabBarInfoContainer}>
        <Image
              source={
                __DEV__
                  ? require('../assets/images/icon.png')
                  : require('../assets/images/icon.png')
              }
              style={styles.welcomeImage}
            />
          <Text style={styles.tabBarInfoText}>Welcome to mSupplier</Text>
        </View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  optionsTitleText: {
    fontSize: 16,
    marginLeft: 15,
    marginTop: 9,
    marginBottom: 12,
  },
  optionIconContainer: {
    marginRight: 9,
  },
  option: {
    backgroundColor: '#fdfdfd',
    paddingHorizontal: 15,
    paddingVertical: 15,
    borderBottomWidth: 1,
    borderBottomColor: '#EDEDED',
  },
  optionText: {
    fontSize: 28,
    marginTop: 1,
  },
  developmentModeText: {
    marginBottom: 20,
    color: 'rgba(0,0,0,0.4)',
    fontSize: 14,
    lineHeight: 19,
    textAlign: 'center',
  },
  contentContainer: {
    paddingTop: 30,
  },
  welcomeContainer: {
    alignItems: 'center',
    marginTop: 10,
    marginBottom: 20,
  },
  welcomeImage: {
    width: 100,
    height: 80,
    resizeMode: 'contain',
    marginTop: 3,
    marginLeft: -10,
  },
  getStartedContainer: {
    alignItems: 'center',
    marginHorizontal: 50,
  },
  homeScreenFilename: {
    marginVertical: 7,
  },
  codeHighlightText: {
    color: 'rgba(96,100,109, 0.8)',
  },
  codeHighlightContainer: {
    backgroundColor: 'rgba(0,0,0,0.05)',
    borderRadius: 3,
    paddingHorizontal: 4,
  },
  getStartedText: {
    fontSize: 17,
    color: 'rgba(96,100,109, 1)',
    lineHeight: 24,
    textAlign: 'center',
  },
  tabBarInfoContainer: {
    position: 'absolute',
    bottom: 0,
    left: 0,
    right: 0,
    ...Platform.select({
      ios: {
        shadowColor: 'black',
        shadowOffset: { height: -3 },
        shadowOpacity: 0.1,
        shadowRadius: 3,
      },
      android: {
        elevation: 20,
      },
    }),
    alignItems: 'center',
    backgroundColor: '#fbfbfb',
    paddingVertical: 20,
  },
  tabBarInfoText: {
    fontSize: 17,
    color: 'rgba(96,100,109, 1)',
    textAlign: 'center',
  },
  navigationFilename: {
    marginTop: 5,
  },
  helpContainer: {
    marginTop: 15,
    alignItems: 'center',
  },
  helpLink: {
    paddingVertical: 15,
  },
  helpLinkText: {
    fontSize: 14,
    color: '#2e78b7',
  },
  scrollStyle:
  {
    marginBottom:150,
  }
});
