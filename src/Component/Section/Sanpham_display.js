import React, { Component } from 'react'
import Header from '../Header';
import Navigation from './Navigation';
import Display_left from './Display_left';
import { Link } from 'react-router-dom';
import { DataContext } from '../Context'
import Footer from './Footer';

class Sanpham_display extends Component {
    static contextType = DataContext;
    state = {
        products: []
    }
    getProduct = () => {
        
        if (this.props.match.params.condition) {
            const result = this.context.products;
            const role = result.map(item =>{
                return item.product_type;
            })
            let data;
            let check = 0;
            if (this.props.match.params.condition === 'all'){
                 data = result.map(item => {
                    return item
                })
            }
            
            else if (this.props.match.params.condition !== 'all') {
                
                role.map(roleItem =>{
                    if (this.props.match.params.condition === roleItem ) {
                        data = result.filter(item => {
                            return item.product_type === this.props.match.params.condition;
                        })
                    }
                    else if  (this.props.match.params.condition !== roleItem )
                        check ++;
                })
                if (check == role.length){
                    data = result.filter(item => {
                        if (item.product_name.toLowerCase().includes((this.props.match.params.condition).toLowerCase()))
                             return item;
                    })
                }
            }
            
            
            this.setState({
                products: data
            })
        }
    }
    
    componentDidMount = () => {
        this.getProduct();
        
    }
    
    componentWillUpdate(nextProps) {
        const result = this.context.products;
        const role = result.map(item =>{
            return item.product_type;
        })
        let data;
        let check = 0;
        if (nextProps.match.params.condition) {

            // Tìm kiếm dựa vào phân loại sản phẩm
            role.map(roleItem =>{
                if (nextProps.match.params.condition === roleItem ) {
                    data = result.filter(item => {
                        return item.product_type === nextProps.match.params.condition;
                    })
                }
                else if  (nextProps.match.params.condition !== roleItem )
                    check ++;
            })
            // Tìm kiếm dựa vào tên sản phẩm
            if (check == role.length){
                data = result.filter(item => {
                    if (item.product_name.toLowerCase().includes((nextProps.match.params.condition).toLowerCase()))
                         return item;
                })
            }
            this.state.products = data
        }
    }
  
    
    render() {
        const { products } = this.state;
        console.log(products);
        return (

            <div>
                <Header></Header>
                <Navigation></Navigation>
                <div className="display_mainContainer grid">
                                    
                    <div className="right_products">
                    
                    {
                        products.map(product => (
                            <div className="item" key={product.id}>
                                <Link to ={`/Sanpham_chiTiet/${product.id}`}>
                                    <img src={product.imageURL}></img>
                                </Link>
                                
                                <div className="item-information">
                                    <div className="item-name-wrap">
                                    <Link className="item-name" to ={`/Sanpham_chiTiet/${product.id}`}>
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

                </div>
                <Footer></Footer> 
            </div>

        );
    }

}
export default Sanpham_display;