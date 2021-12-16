import React, { Component } from 'react'
import { Link } from 'react-router-dom';
import { DataContext } from './Context';
import Account_Operation from './Section/Account_Operation';
class Header extends Component {
    static contextType = DataContext;
    state = {
        form: false,
        find_value: ''
    }

    showForm = () => {
        this.setState({
            form: !this.state.form
        })
        
    }
    setfindValue = (val) =>{
        this.setState({
            find_value: val.target.value
        })
    }
    render() {
        const { cart } = this.context;
       
        if (this.state.form === true) {
            return (
                <div>
                    <header>
                    <div className="main-container grid">
                        <a href="#" className="homeReload">
                            <Link to="/">
                                <img src="/img/logo.png" alt="" />
                            </Link>

                        </a>
                        <div className="searchArea">
                            <input onChange={this.setfindValue} type="text" name id="searchBar" placeholder="Tìm kiếm sản phẩm"/>
                            <Link exact to={`/Sanpham_display/${this.state.find_value}`} >
                                <i class="fas fa-search"></i>
                            </Link>
                            
                        </div>
                        
                        <div className="operation">

                            <div onClick={() => this.showForm()} href className="account">
                                <i className="far fa-user" />
                            </div>

                            <Link to="/Cart">
                                <div href className="cart">
                                    <i id="cart_icon" className="fas fa-shopping-cart" />
                                    <div className="cart_number">{cart.length}</div>
                                </div>
                            </Link>


                        </div>
                    </div>
                    </header>
                    <Account_Operation></Account_Operation>
                    
                </div>
            )
        }
        else if (this.state.form === false) {
            return (
                <header>
                    <div className="main-container grid">
                        <a href="#" className="homeReload">
                            <Link to="/">
                                <img src="/img/logo.png" alt="" />
                            </Link>

                        </a>
                        <div className="searchArea">
                            <input onChange={this.setfindValue} type="text" name id="searchBar" placeholder="Tìm kiếm sản phẩm"/>
                            <Link exact to={`/Sanpham_display/${this.state.find_value}`} >
                                <i class="fas fa-search"></i>
                            </Link>
                            
                        </div>
                        
                        <div className="operation">

                            <div onClick={() => this.showForm()} href className="account">
                                <i className="far fa-user" />
                            </div>

                            <Link to="/Cart">
                                <div href className="cart">
                                    <i id="cart_icon" className="fas fa-shopping-cart" />
                                    <div className="cart_number">{cart.length}</div>
                                </div>
                            </Link>


                        </div>
                    </div>
                </header>
            );
        }

    }

}

export default Header;