import React, { Component } from 'react'
import '../icon/css/all.min.css';
class DisplayAll_left extends Component {

    constructor(props) {
        super(props);
        this.state = {
            status1: true,
            status2: true,
            status3: true,
            status4: true,
        }
    }
    displayMenu1 = () => {
        this.setState({
            status1: !this.state.status1
        })
        if (this.state.status1 === false) {
            document.querySelector("#dropdown1").classList.add("show")
        }
        else {
            document.querySelector("#dropdown1").classList.remove("show")
        }
    }
    displayMenu2 = () => {
        this.setState({
            status2: !this.state.status2
        })
        if (this.state.status2 === false) {
            document.querySelector("#dropdown2").classList.add("show")
        }
        else {
            document.querySelector("#dropdown2").classList.remove("show")
        }
    }
    displayMenu3 = () => {
        this.setState({
            status3: !this.state.status3
        })
        if (this.state.status3 === false) {
            document.querySelector("#dropdown3").classList.add("show")
        }
        else {
            document.querySelector("#dropdown3").classList.remove("show")
        }
    }
    displayMenu4 = () => {
        this.setState({
            status4: !this.state.status4
        })
        if (this.state.status4 === false) {
            document.querySelector("#dropdown4").classList.add("show")
        }
        else {
            document.querySelector("#dropdown4").classList.remove("show")
        }
    }
    render() {


        return (

            <div className="left_optionals">
                <div onClick={() => this.displayMenu1()} className="optionals_item productBrand">
                    Thương hiệu
                    <i className="fas fa-plus"></i>

                </div>
                <div id="dropdown1" className="optionals_item_dropdown show">
                    <div className="checkItem">
                        <input type="checkbox" id="Nike_Zoom" />
                        <label for="Nike_Zoom"> Nike Zoom</label>
                    </div>
                    <div className="checkItem">
                        <input type="checkbox" id="Nike_Air" />
                        <label for="Nike_Air"> Nike Air</label>
                    </div>



                </div>

                <div onClick={() => this.displayMenu2()} className="optionals_item productPrice">
                    Giá
                    <i className="fas fa-plus"></i>
                </div>
                <div id="dropdown2" className="optionals_item_dropdown show">
                    <div className="checkItem">
                        <input name="price" type="radio" id="lessThan_1M" />
                        <label for="lessThan_1M">Nhỏ hơn 1,000,000đ</label>
                    </div>
                    <div className="checkItem">
                        <input name="price"  type="radio" id="1M_2M" />
                        <label for="1M_2M">Từ 1,000,000đ đến 2,000,000đ</label>
                    </div>
                    <div className="checkItem">
                        <input name="price"  type="radio" id="2M_3M" />
                        <label for="2M_3M">Từ 2,000,000đ đến 3,000,000đ</label>
                    </div>
                    <div className="checkItem">
                        <input name="price"  type="radio" id="3M_4M" />
                        <label for="3M_4M">Từ 3,000,000đ đến 4,000,000đ</label>
                    </div>
                    <div className="checkItem">
                        <input name="price"  type="radio" id="over_4M" />
                        <label for="over_4M">Lớn hơn 4,000,000đ</label>
                    </div>
                </div>

                <div onClick={() => this.displayMenu3()} className="optionals_item productColor">
                    Màu sắc
                    <i className="fas fa-plus"></i>
                </div>
                <div id="dropdown3" className="optionals_item_dropdown show">
                    <div style={{backgroundColor: 'white'}} className="color_item white"></div>
                    <div style={{backgroundColor: 'black'}} className="color_item black"></div>
                    <div style={{backgroundColor: '#f44336'}} className="color_item red "></div>
                    <div style={{backgroundColor: '#f06292'}} className="color_item pink"></div>
                    <div style={{backgroundColor: '#9c27b0'}} className="color_item purple"></div>
                    <div style={{backgroundColor: '#2196f3'}} className="color_item Light_blue"></div>
                    <div style={{backgroundColor: '#3f51b5'}} className="color_item Heavy_blue"></div>
                    <div style={{backgroundColor: '#57b45b'}} className="color_item green "></div>
                    <div style={{backgroundColor: '#ffb74d'}} className="color_item Heavy_yellow"></div>
                    <div style={{backgroundColor: '#cfd8dc'}} className="color_item grey"></div>
                    <div style={{backgroundColor: '#8d6e63'}} className="color_item brown"></div>
                    <div style={{backgroundColor: '#ffee58'}} className="color_item Light_yellow"></div>
                </div>

                <div onClick={() => this.displayMenu4()} className="optionals_item productSize">
                    Kích thước
                    <i className="fas fa-plus"></i>
                </div>
                <div id="dropdown4" className="optionals_item_dropdown show">
                    <ul className="sizeList">
                        <li className="sizeList_item">
                            <input name="size35" type="checkbox" id="size35" />
                            <label for="size35">35</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size36" type="checkbox" id="size36" />
                            <label for="size36">36</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size37" type="checkbox" id="size37" />
                            <label for="size37">37</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size38" type="checkbox" id="size38" />
                            <label for="size38">38</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size39" type="checkbox" id="size39" />
                            <label for="size39">39</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size40" type="checkbox" id="size40" />
                            <label for="size40">40</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size41" type="checkbox" id="size41" />
                            <label for="size41">41</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size42" type="checkbox" id="size42" />
                            <label for="size42">42</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size43" type="checkbox" id="size43" />
                            <label for="size43">43</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size44" type="checkbox" id="size44" />
                            <label for="size44">44</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size45" type="checkbox" id="size45" />
                            <label for="size45">45</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size46" type="checkbox" id="size46" />
                            <label for="size46">46</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size47" type="checkbox" id="size47" />
                            <label for="size47">47</label>
                        </li>
                        <li className="sizeList_item">
                            <input name="size48" type="checkbox" id="size48" />
                            <label for="size48">48</label>
                        </li>
                    </ul>
                </div>
            </div>


        );
    }

}
export default DisplayAll_left;