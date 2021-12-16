import React, { Component } from 'react'
import { Link } from 'react-router-dom';
import { AccountContext } from '../Context'
import { DataContext } from '../Context'

class SignIn extends Component {

    static contextType = DataContext;
    state = {
        username: '',
        password: '',
        error: 'abcdxyz',
        
    }

    setName = (val) => {
        this.setState({
            username: val.target.value
        })

    }

    setPassword = (val) => {
        this.setState({
            password: val.target.value
        })

    }

    checkLogged = () => {
        console.log(this.context.account);
        if (typeof this.context.account.data !== 'undefined') {
            return true
        }
        else return false
    }

    
    getAccount = (array) => {

        var errorElement = document.getElementById("error-area");
        const data = array.filter(item => {
            if ((this.state.username === item.customer_phoneNumber || this.state.username === item.customer_email) && this.state.password === item.customer_password)
                return item
        })

        if (data.length === 0) {
            this.setState({
                error: "Sai tên đăng nhập hoặc mật khẩu",
            })
            errorElement.style.color = "red";
        }
        else {
            errorElement.style.color = "#f4f4f4";
            this.context.Login(this.state.username, this.state.password)
            alert("Đăng nhập thành công !")
        }

    }


    render() {
        const { clients, account } = this.context;
        
        if (this.checkLogged() === true) {
            return (
                <div className="Information">
                    <h2>THÔNG TIN KHÁCH HÀNG</h2>
                    <>
                        {
                            account.data.map(item => (
                                <div>
                                    <div className="info">
                                        <span>Tên khách hàng: </span>
                                        <span className="info-data">{item.customer_name} </span>
                                    </div>
                                    <div className="info">
                                        <span>Số điện thoại: </span>
                                        <span className="info-data">{item.customer_phoneNumber} </span>
                                    </div>
                                    <div className="info">
                                        <span>Địa chỉ: </span>
                                        <span className="info-data">{item.customer_address} </span>
                                    </div>
                                    <div className="info">
                                        <span>E-mail: </span>
                                        <span className="info-data">{item.customer_email} </span>
                                    </div>

                                    <Link to="/Order_History" >
                                        <div className="order-history"><u>Xem lịch sử đặt hàng</u></div>
                                    </Link>
                                    

                                    <div className="btn-area">
                                        <button onClick={() => this.context.Logout()}>Đăng xuất</button>
                                    </div>

                                </div>

                            ))

                        }
                    </>
                </div>
            )
        }

        else if (this.checkLogged() === false) {
            return (
                <AccountContext.Consumer>{(accountContext) => {
                    const { switchToSignUp } = accountContext
                    return (
                        <div className="signIn-container">
                            <h2>Đăng Nhập</h2>
                            <div className="login-name">
                                <i class="fas fa-user"></i>
                                <input onChange={this.setName} type="text" name id="loginName" placeholder="Email hoặc Số điện thoại" />
                            </div>
                            <div className="login-password">
                                <i class="fas fa-unlock-alt"></i>
                                <input onChange={this.setPassword} type="password" name id="loginPassword" placeholder="Mật khẩu" />
                            </div>
                            <div id="error-area">{this.state.error}</div>
                            <div className="btn-area">
                                <button onClick={() => this.getAccount(clients)}>Đăng nhập</button>
                            </div>
                            
                            <div className="optional">
                                <span>Bạn chưa có tài khoản ?</span>
                                <span className="direct" onClick={switchToSignUp}>Đăng ký</span>
                            </div>
                        </div>
                    )

                }}
                </AccountContext.Consumer>



            );
        }

    }
}
export default SignIn;

