

import React, { Component } from 'react'
import { Route, Switch } from "react-router-dom"
import Sanpham_display from './Section/Sanpham_display';
import Homepage from './Homepage';
import Gioithieu from './Section/Gioithieu';
import Sanpham_chiTiet from './Section/Sanpham_chiTiet';
import Cart from './Section/Cart';
import Order_History from './Section/Order_History';



class Section extends Component {
    render() {
        return (
            <Switch>
                <Route exact path="/" component={Homepage}></Route>
                <Route exact path="/Sanpham_display/:condition" component={Sanpham_display}></Route>
                <Route exact path="/Gioithieu" component={Gioithieu}></Route>
                <Route path="/Sanpham_chiTiet/:id" component={Sanpham_chiTiet}></Route>
                <Route path="/Cart" component={Cart}></Route>
                <Route path="/Order_History" component={Order_History}></Route>
            </Switch>
        );
    }

}

export default Section;
