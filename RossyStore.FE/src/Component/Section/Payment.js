import React, { Component } from 'react'
import Header from '../Header';
import Navigation from './Navigation';
import Footer from './Footer';
import { Link } from 'react-router-dom';
import { DataContext } from '../Context'

class Cart extends Component {
    static contextType = DataContext;
    render() {
        const { cart } = this.context;
        return (
            <div>
                <Header></Header>
                <Navigation></Navigation>
                Payment
                <Footer></Footer>
            </div>

        );
    }

}
export default Cart;