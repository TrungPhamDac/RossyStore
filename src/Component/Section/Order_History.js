import React, { Component } from 'react'
import Header from '../Header';
import Navigation from './Navigation';
import Footer from './Footer';
import axios from 'axios';

import { DataContext } from '../Context'

class Order_History extends Component {

    static contextType = DataContext;
    state = {
        orders: []
    }
    componentDidMount() {
        const cus_phone = this.context.account.data[0].customer_phoneNumber;
        axios({
            method: 'GET',
            url: `https://localhost:44328/api/Orders/GetOrder_ByPhone/${cus_phone}`,
            data: null
        }).then(res => {
            console.log(res.data);
            this.setState({
                orders: res.data
            })
        })

    }
    render() {

        const { orders } = this.state
        return (
            <div>
                <Header></Header>
                <Navigation></Navigation>
                <div className="order-container grid">
                    <h3>ĐƠN HÀNG ĐÃ ĐẶT</h3>
                    <div className="main-container">
                        {
                            orders.map(order_item => (
                                <div className="order-details">
                                    <h3 className="order_id">{order_item.id}</h3>
                                    <div className="order-products">
                                        {
                                            order_item.list_cart.map(item => (
                                                <div>
                                                    
                                                    <div className="cartItem_mainContainer" key={order_item.id}>

                                                        <div className="img-main">
                                                            <img src={this.context.getImgbyID(item.id)}></img>
                                                        </div>
                                                        <div className="information">
                                                            <h3 className="name">{this.context.getNamebyID(item.id)}</h3>
                                                            <div className="separate"></div>
                                                            <h2 className="price">{this.context.getMultiplePrice(item.id, item.quantity)}đ</h2>
                                                            <h3 className="color">Màu sắc: {item.product_detail_color}</h3>
                                                            <h3 className="size">Kích cỡ: {item.product_detail_size}</h3>
                                                            <div className="amount">
                                                                <span> Số lượng: {item.quantity} </span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            ))
                                        }
                                    </div>
                                    <h3 className="order_total">Tổng tiền: {order_item.order_total}</h3>
                                </div>
                            ))
                        }
                    </div>
                </div>
                <Footer></Footer>
            </div>
        )
    }

}
export default Order_History;