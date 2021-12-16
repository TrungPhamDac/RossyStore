import React, { Component } from 'react'
import axios from 'axios';
import Validator from './Form_Validation'
import { AccountContext } from '../Context'

class SignUp extends Component {

    static contextType = AccountContext;
    state = {
        name: '',
        email: '',
        phone: '',
        address: '',
        password: '',
        re_password: '',

    }
    setName = (val) => {
        this.setState({
            name: val.target.value
        })
    }
    
    setEmail = (val) => {
        this.setState({
            email: val.target.value
        })
    }
    setPhone = (val) => {
        this.setState({
            phone: val.target.value
        })
    }
    setAddress = (val) => {
        this.setState({
            address: val.target.value
        })

    }
    setPassword = (val) => {
        this.setState({
            password: val.target.value
        })
    }
    setRePassword = (val) => {
        this.setState({
            re_password: val.target.value
        })
    }

    handleSignUp = event => {
        
        axios({
            method: 'POST',
            url: 'https://localhost:44328/api/Customers',
            data: {
                id: "",
                customer_name: this.state.name,
                customer_email: this.state.email,
                customer_phoneNumber: this.state.phone,
                customer_password: this.state.password,
                customer_address: this.state.address
            }
        });
        var form = document.querySelector("#signUp-form");
        form.reset();  // Reset all form data
        alert("Đăng kí thành công")
        event.preventDefault();

    }

    render() {
        
        const { switchToSignIn } = this.context

        return (
            <div className="signUp-container">
                <form id="signUp-form" onSubmit={this.handleSignUp} >
                    <h2>Đăng Ký</h2>
                    <div className="name">
                        <i class="fas fa-user"></i>
                        <input onChange={this.setName} type="text" name id="name" placeholder="Họ và tên" />
                        <h3 className="message error"></h3>
                    </div>

                    <div className="email">
                        <i class="fas fa-envelope"></i>
                        <input onChange={this.setEmail} type="text" name id="email" placeholder="Email" />
                    </div>
                    <div className="phoneNumber">
                        <i class="fas fa-phone"></i>
                        <input onChange={this.setPhone} type="text" name id="phoneNumber" placeholder="Số điện thoại" />
                    </div>

                    <div className="address">
                        <i class="fas fa-home"></i>
                        <input onChange={this.setAddress} type="text" name id="address" placeholder="Địa chỉ" />
                    </div>

                    <div className="signup-password">
                        <i class="fas fa-unlock-alt"></i>
                        <input onChange={this.setPassword} type="password" name id="signupPassword" placeholder="Mật khẩu" />
                    </div>
                    <div className="signup-passwordConfirm">
                        <i class="fas fa-unlock-alt"></i>
                        <input onChange={this.setRePassword} type="password" name id="signupPassword" placeholder="Nhập lại mật khẩu" />
                    </div>
                    <div className="btn-area">
                        <button id="btn-signUp" type="submit"  >Đăng Ký </button>
                    </div>

                    <div className="optional">
                        <span>Bạn đã có tài khoản ?</span>
                        <span className="direct" onClick={switchToSignIn}>Đăng nhập</span>

                    </div>
                </form>
            </div>


        );

    }
}
export default SignUp;
