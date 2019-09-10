import React from 'react';
import PropTypes from 'prop-types';
import {StyleSheet ,  View, TextInput,  Image,Text,TouchableHighlight } from 'react-native';

const BLUE = "#428AF8";
const LIGHT_GRAY= "#D3D3D3";
class InventoryItem extends React.Component {
  onAddClick = () => {
    // No bind needed since we can compose 
    // the relevant data for this item here
    this.props.item.qty = this.state.qty;
    this.props.item.unitPrice = this.state.unitPrice;
    this.props.onClick(this.props.item);
  }

  constructor(props)
  {
      super(props);     
     this.state = {
        qty: this.props.item.qty,
        unitPrice: this.props.item.unitPrice,
        images:[require('../assets/images/carrots.png'),
        require('../assets/images/leeks.png'),
        require('../assets/images/beans.png'),
        require('../assets/images/potatoes.png'),
        require('../assets/images/tomatoes.png')]
    };
  }

  // No arrow func in render! 👍
  render() {
    return (
        <View style={[styles.slide]}>

          <View style={styles.welcomeContainer}>
            <Text style={styles.text}>{this.props.item.name}</Text>
            <Image source= {this.state.images[this.props.item.id -1]} style={styles.welcomeImage}/>
          </View>

            <TextInput keyboardType='numeric' style={styles.text} 
                 placeholder='Quantity' onChangeText={(qty) => this.setState({qty})}
                value={this.state.qty} underlineColorAndroid={BLUE}/>
                    
            <TextInput keyboardType='numeric' style={styles.text}
                 placeholder='Unit Price' onChangeText={(unitPrice) => this.setState({unitPrice})}
                value={this.state.unitPrice} underlineColorAndroid={BLUE}/>
               
                 
                <TouchableHighlight onPress={this.onAddClick} underlayColor={LIGHT_GRAY}>
                 <View style={styles.button}>
                    <Text style={styles.buttonText}>ADD</Text>
                 </View>
               </TouchableHighlight>
             
        </View>
    );
  }
}

InventoryItem.propTypes = {
  item: PropTypes.object.isRequired,
  onClick: PropTypes.func.isRequired
};

const styles = StyleSheet.create({

    slide: {
      padding: 15,
      flex: 1,
    },
  
    text: {
      fontSize: 36,
      padding:10
    }, 
    
    welcomeContainer: {
        alignItems: 'center',
        marginBottom: 20,
      },
      welcomeImage: {
        width: 200,
        height:160,
        resizeMode: 'contain',
        marginTop: -10,
        marginLeft: -10,
      },
      button: {
        marginBottom: 30,
        alignItems: 'center',        
        backgroundColor:'#428AF8'
      },
      buttonText: {
        padding: 10,
        fontSize:36,
        color: 'white'
      }
  });

export default InventoryItem;