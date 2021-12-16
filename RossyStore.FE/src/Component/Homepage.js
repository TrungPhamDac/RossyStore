

import React, { Component } from 'react'

import Header from './Header';
import Navigation from './Section/Navigation';
import Firstslider from './Section/Firstslider';
import Service from './Section/Service';
import Sanphammoi from './Section/Sanphammoi';
import Sanphamhot from './Section/Sanphamhot';
import Thuonghieu from './Section/Thuonghieu';
import Footer from './Section/Footer';

class Homepage extends Component {
    
    render() {
       
        return (           
                <div>
                    <Header></Header>
                    <Navigation></Navigation>
                    <Firstslider></Firstslider>
                    <Service></Service>
                    <Sanphammoi></Sanphammoi>
                    <Sanphamhot ></Sanphamhot>
                    <Thuonghieu></Thuonghieu>
                    <Footer></Footer>
                </div>
        );
    }
}

export default Homepage;
