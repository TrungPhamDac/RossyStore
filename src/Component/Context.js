import React, { Component } from 'react'
import callApi from './Callapi';
import axios from 'axios';
export const DataContext = React.createContext("1");
export const AccountContext = React.createContext("2");


export class DataProvider extends Component {
    
    state = {
        products: [],
        clients: [],
        cart : [],
        account:[],
        order_history: [],
        total: 0
    }
    componentDidMount(){
        axios({
            method: 'GET',
            url: 'https://localhost:44328/api/Products/GetAll',
            data: null
        }).then(res =>{
            this.setState({
                products: res.data
            })
        })

        axios({
            method: 'GET',
            url: 'https://localhost:44328/api/Customers/GetAll',
            data: null
        }).then(res =>{
            this.setState({
                clients: res.data
            })
        })
    }
    
   /*  componentWillUpdate() { // immmediately update employee after signup without refreshing
        callApi('employee', "GET", null).then(res=>{
            this.setState({
                employee: res.data
            })
        })

    } */
    
    addtoCart = (id, color, size) =>{
        
        const {products, cart} = this.state;
        const check = cart.every(item=>{
            if (id === item.id && color === item.product_detail_color && item.product_detail_size === size)
                return false;
            else return true                   
        })
        
        if (check) {
            const data = products.filter(item=>{
                return item.id === id
            })
            const array = data
            const index = cart.length;
            const quantity = 1; 
            const product_detail_color = color
            const product_detail_size = size
            this.setState({
                cart: [...cart, {index, array, id, product_detail_color, product_detail_size, quantity}]
            })
                     
            alert("Thêm sản phẩm thành công")

            console.log(cart);
        }
        else alert("Sản phẩm đã tồn tại trong giỏ hàng")
    }

    getPrice = (id) => {
        let str_price = "";
        this.state.products.forEach((item) => {
            if (item.id === id)
                str_price = item.product_price.toString()
        })

        str_price = str_price.slice(0, 1) + "," + str_price.slice(1)
        str_price = str_price.slice(0, 5) + "," + str_price.slice(5)

        return str_price;
    }
    increase = (index) =>{
        const {cart} = this.state;
        cart.forEach(item=>{
            if (item.index === index)
                item.quantity += 1;
        })
        this.getTotal();
        this.setState({cart: cart})
    }

    decrease = (index) =>{
        const {cart} = this.state;
        cart.forEach(item=>{
            if (item.index === index)
                item.quantity === 1 ? item.quantity = 1 : item.quantity -=1;
        })
        this.getTotal();
        this.setState({cart: cart})
    }

    remove = (indexOfitem) =>{
        const {cart} = this.state;
        cart.forEach((item, index)=>{
            if (item.index === indexOfitem)
                cart.splice(index, 1)
        })
        this.getTotal();
        this.setState({cart: cart})
    }

    getMultiplePrice = (id, quantity) =>{
        let str_price = "";
        this.state.products.forEach((item) => {
            if (item.id === id)
                str_price = (item.product_price*quantity).toString()
        })
        if (str_price.length === 7) {
            //Chuõi giá tiền = 7 thì chèn , vô chỗ số 1 và 5
            str_price = str_price.slice(0, 1) + "," + str_price.slice(1)
            str_price = str_price.slice(0, 5) + "," + str_price.slice(5)
        }
        else  if (str_price.length > 7){
            //Chuỗi giá tiền > 7 thì chèn , vô chỗ số 1 + i và 5 + i
            let i = str_price.length - 7
            str_price = str_price.slice(0, 1+i) + "," + str_price.slice(1+i)
            str_price = str_price.slice(0, 5+i) + "," + str_price.slice(5+i)
        } 

        return str_price;
    }

    getTotal = () =>{
        const {cart} = this.state;
        if (cart.length == 0) {
            this.setState({
                total: 0
            }) 
        }
        else {
            let res = 0;
            cart.forEach((cartitem) => {
                cartitem.array.forEach(item =>{
                    res = res + (item.product_price * cartitem.quantity)
                })             
            })    
            let str_price = res.toString(); 
            
            if (str_price.length === 7) {
                //Chuõi giá tiền = 7 thì chèn , vô chỗ số 1 và 5
                str_price = str_price.slice(0, 1) + "," + str_price.slice(1)
                str_price = str_price.slice(0, 5) + "," + str_price.slice(5)
            }
            else  if (str_price.length > 7){
                //Chuỗi giá tiền > 7 thì chèn , vô chỗ số 1 + i và 5 + i
                let i = str_price.length - 7
                str_price = str_price.slice(0, 1+i) + "," + str_price.slice(1+i)
                str_price = str_price.slice(0, 5+i) + "," + str_price.slice(5+i)
            } 
            
            
            this.setState({
                total: str_price
            }) 
        }
        
    }
    Login = (username, password) => {
        const { clients} = this.state
        const data = clients.filter(item => {
            if  ( username === item.customer_phoneNumber || username === item.customer_email  && password===item.customer_password )
                return item
        })
        this.setState({
            account: {data}
        })
    
    }
    Logout = () =>{
        const { account} = this.state
        this.setState({
            account : {}
        })
        
    }
    ClearCart = () =>{
        const { cart} = this.state
        this.setState({
            cart : []
        })
    }

    getNamebyID = (id) => {
        let name = ''
        this.state.products.forEach((item) => {
            if (item.id === id)
                name = item.product_name
        })
        return name
    }

    getImgbyID = (id) => {
        let url = ''
        this.state.products.forEach((item) => {
            if (item.id === id)
                url = item.imageURL
        })
        return url
    }
 
    render() {
        const {products, cart, account, clients, total, order_history} = this.state;
        const {addtoCart, getPrice, increase, decrease, remove, getMultiplePrice, getTotal ,Login, Logout, ClearCart, getNamebyID, getImgbyID} = this;
        return (
            <DataContext.Provider value={{products,cart,account, clients, total, order_history, addtoCart, getPrice,increase, decrease, remove, getMultiplePrice, getTotal,Login, Logout, ClearCart, getNamebyID, getImgbyID} }>
                {this.props.children}
            </DataContext.Provider>
        );
    }
}

