import React, { Component } from 'react'

class Thuonghieu extends Component {
    render() {
        return (
            <div className="Brands">
                <div className="title-area grid">
                    <div className="left-separate"></div>
                    <div className="title-content">
                        <h3>THƯƠNG HIỆU</h3>
                        <p>Thương hiệu nổi bật của chúng tôi</p>
                    </div>
                    <div className="right-separate"></div>
                </div>
                <div className="brand-item grid">
                    <img src="/img/partner_1.png"></img>
                    <img src="/img/partner_2.png"></img>
                    <img src="/img/partner_3.png"></img>
                    <img src="/img/partner_4.png"></img>
                    <img src="/img/partner_5.png"></img>
                    <img src="/img/partner_6.png"></img>
                    <img src="/img/partner_7.png"></img>
                    <img src="/img/partner_8.png"></img>
                </div>
            </div>
        );
    }

}

export default Thuonghieu;