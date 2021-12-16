import React, { Component } from 'react'
import '../icon/css/all.min.css';

class Footer extends Component {
    render() {
        return (
            <div className="footer">
                <div className="footer-container grid">
                    <div className="footer-container-item contact">
                        <h3>LIÊN HỆ</h3>
                        <div className="separate"></div>
                        <p className="address">Khu phố 6, phường Linh Trung, TP. Thủ Đức, TP. Hồ Chí Minh</p>
                        <p className="phone">Phone: 0938559501</p>
                        <p className="email">Email: uit.edu.vn@gmail.com</p>
                    </div>
                    <div className="footer-container-item policy">
                        <h3>CHÍNH SÁCH HỖ TRỢ</h3>
                        <div className="separate"></div>
                        <a href="#">Chính sách đổi sản phẩm</a>
                        <a href="#">Chính sách trả góp</a>
                        <a href="#">Chính sách bảo hành</a>
                        <a href="#">Chính sách giao hàng</a>
                    </div>
                    <div className="footer-container-item link">
                        <h3>LIÊN KẾT</h3>
                        <div className="separate"></div>
                        <p>Hãy kết nối với chúng tôi</p>
                        <div class="social-flatform">
                            <i class="fab fa-facebook-f"></i>
                            <i class="fab fa-twitter"></i>
                            <i class="fab fa-google-plus-g"></i>
                            <i class="fab fa-pinterest"></i>
                        </div>
                        <img src="/img/dkbocongthuong.png"></img>
                    </div>
                    <div className="footer-container-item transfers">
                        <h3>THÔNG TIN CHUYỂN KHOẢN</h3>
                        <div className="separate"></div>
                        <p>BIDV:   1331 00000 74767</p>
                        <p>Momo:   0934 189 092</p>
                        <p>Zalopay:   0934 189 092</p>
                        <p>Payoo:   0934 189 092</p>
                    </div>
                </div>
                <div className="longSeparate"></div>
                <p className="copyRight">© 2021 All rights reserved. ROSSY STORE</p>
            </div>
        );
    }

}

export default Footer;