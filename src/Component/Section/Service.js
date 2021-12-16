import React, { Component } from 'react'
import '../icon/css/all.min.css';
class Service extends Component {
    render() {
        return (
            <div className="service">
                <div className="main-container grid">
                    <div className="main-container grid">
                        <div className="main-container-item delivery">
                            <p>
                                <i className="fas fa-truck"></i>
                            </p>
                            <h3>GIAO HÀNG MIỄN PHÍ</h3>
                            <p>Tất cả sản phẩm đều được vận chuyển miễn phí</p>
                        </div>
                        <div className="main-container-item refund">
                            <p>
                                <i className="fas fa-undo-alt"></i>
                            </p>
                            <h3>ĐỔI TRẢ HÀNG</h3>
                            <p>Sản phẩm được phép đổi trả trong vòng 2 ngày</p>
                        </div>
                        <div className="main-container-item cod">
                            <p>
                                <i className="fas fa-hand-holding-usd"></i>
                            </p>

                            <h3>THANH TOÁN NHẬN HÀNG</h3>
                            <p>Thanh toán đơn hàng bằng hình thức trực tiếp</p>
                        </div>
                        <div className="main-container-item order">
                            <p>
                                <i className="fas fa-phone-alt"></i>
                            </p>

                            <h3>ĐẶT HÀNG ONLINE</h3>
                            <p>0934 189 106</p>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

}

export default Service;