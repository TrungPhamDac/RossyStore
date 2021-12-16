import React, { Component } from 'react'
import Header from '../Header';
import Navigation from './Navigation';
import Footer from './Footer';
import { Link } from 'react-router-dom';
import { DataContext } from '../Context'

class Sanpham_chiTiet extends Component {
    static contextType = DataContext;
    state = {
        product: [],
        size: "",
        color: ""

    }

    getPrice = () =>{
        
    }
    getProduct = () => {
        
        if (this.props.match.params.id) {
            const result = this.context.products;
            
            const data = result.filter(item => {
                return item.id === this.props.match.params.id;
            })
            this.setState({
                product: data
            })
        }
        
    }
    componentDidMount = () => {
        this.getProduct();
    }

    changeImg = (key) => {
        var url;
        var optionImg = document.getElementsByClassName("img-choose-item");
        var mainImg = document.querySelector(".img-main img");
        for (var i = 0; i <= optionImg.length; i++) {
            if (i === key) {
                url = optionImg[i].src;
            }

        }
        mainImg.src = url;
    }
    chooseColor = (key) => {
        var optionColor = document.getElementsByClassName("color_options");
            for (var i = 0; i < optionColor.length; i++) {
                optionColor[i].style.borderColor = "red";
                optionColor[i].style.borderWidth = "1px";
                optionColor[i].style.lineHeight = "35px";
                if (i === key) {
                    optionColor[i].style.borderColor = "#58b3f0";
                    optionColor[i].style.borderWidth = "3px";
                    optionColor[i].style.lineHeight = "29px";
                    this.state.color = optionColor[i].innerHTML;

                }
            }
        
    }

    chooseSize = (key) => {
        var optionSize = document.getElementsByClassName("size_options");
        for (var i = 0; i < optionSize.length; i++) {
            optionSize[i].style.borderColor = "red";
            optionSize[i].style.borderWidth = "1px";
            optionSize[i].style.lineHeight = "35px";
            if (i === key) {
                optionSize[i].style.borderColor = "#58b3f0";
                optionSize[i].style.borderWidth = "3px";
                optionSize[i].style.lineHeight = "29px";

                this.state.size = optionSize[i].innerHTML;

            }

        }

    }

    render() {
        
        const { product } = this.state;


        return (
            <div>
                <Header></Header>
                <Navigation></Navigation>
                <>
                    {
                        product.map(item => (
                            <div className="chiTiet_mainContainer grid" key={item.id}>
                                <div className="img-choose">
                                    {
                                        item.product_images.map((imgItem, key) => (
                                            <img id={key} onClick={() => this.changeImg(key)} className="img-choose-item" src={imgItem}></img>
                                        ))
                                    }
                                </div>
                                <div className="img-main">
                                    <img src={item.product_images[0]}></img>
                                </div>
                                <div className="operation">
                                    <h3 className="name">{item.product_name}</h3>
                                    <div className="separate"></div>
                                    <h2 className="price">{this.context.getPrice(item.id)}đ</h2>
                                    <div className="colorArea">
                                        <span>Màu sắc</span>
                                        {
                                            item.product_colors.map((colorItem, key) => (
                                                <div id={key} onClick={() => this.chooseColor(key)} className="color_options">{colorItem}</div>
                                            ))
                                        }

                                    </div>
                                    <div className="sizeArea">
                                        <span>Kích thước</span>
                                        {
                                            item.product_sizes.map((sizeItem, key) => (
                                                <div id={key} onClick={() => this.chooseSize(key)} className="size_options">{sizeItem}</div>
                                            ))
                                        }
                                    </div>
                                    <div className="action">
                                        <div onClick={() => this.context.addtoCart(item.id, this.state.color, this.state.size)} className="addtoCart">
                                            <div className="cart">
                                                <i class="fas fa-cart-plus"></i>
                                            </div>
                                            <div className="addtoCart_action">THÊM VÀO GIỎ</div>
                                        </div>

                                        <Link to="/Cart">
                                            <div className="buyNow">
                                                <div className="check">
                                                    <i class="fas fa-check"></i>
                                                </div>
                                                <div className="buyNow_action">MUA HÀNG</div>
                                            </div>
                                        </Link>

                                    </div>
                                </div>
                                <img className="statement" src="/img/statement.png"></img>
                            </div>
                        ))
                    }
                </>
                <Footer></Footer>
            </div>

        );
    }

}
export default Sanpham_chiTiet;