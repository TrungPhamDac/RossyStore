import React, { Component } from 'react'
import { Link } from 'react-router-dom';
import { DataContext } from '../Context'
class Sanphammoi extends Component {
    static contextType = DataContext;
    render() {
        const { products } = this.context;
        return (
            <div className="newItem">
                <div className="title-area grid">
                    <div className="left-separate"></div>
                    <div className="title-content">
                        <h3>SẢN PHẨM MỚI</h3>
                        <p>Hàng luôn được cập nhật thường xuyên</p>
                    </div>
                    <div className="right-separate"></div>
                </div>
                <div className="newItem-mainContainer grid">
                    {
                        products.map(product => (
                            <div className="item">
                                <Link to={`/Sanpham_chiTiet/${product.id}`}>
                                    <img src={product.imageURL}></img>
                                </Link>
                                <div className="item-information">
                                    <div className="item-name-wrap">
                                        <Link className="item-name" to={`/Sanpham_chiTiet/${product.id}`}>
                                            {product.product_name}
                                        </Link>
                                    </div>
                                    <div className="item-purpose">{product.product_type}</div>
                                    <div className="item-separate"></div>
                                    <div className="item-price">{this.context.getPrice(product.id)}đ</div>
                                </div>
                            </div>
                        ))
                    }
                </div>
                <div className="btnArea">
                    <Link to={`/Sanpham_display/all`}>
                        <button className="showAll-btn">
                            Xem tất cả
                        </button>
                    </Link>
                </div>

            </div>
        );
    }

}

export default Sanphammoi;