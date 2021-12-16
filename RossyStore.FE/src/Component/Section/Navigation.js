import React, { Component } from 'react'
import '../icon/css/all.min.css';
import { Link } from 'react-router-dom';
class Navigation extends Component {
  
    render() {
        return (
            <div className="navigation">
                <ul className="list">
                    <li className="item">
                        <Link to="/">
                            <a >TRANG CHỦ</a>
                        </Link>

                    </li>
                    <li className="item">
                        <Link to={"./Gioithieu"}>
                            <a >GIỚI THIỆU</a>
                        </Link>

                    </li>
                    <li className="item">
                        <a href>
                            GIÀY NAM
                            <i className="fas fa-chevron-down" />
                        </a>
                        <div className="dropdownList">
                            <div className="dropdownList_item">
                                <Link exact to={`/Sanpham_display/Lifestyle`} >
                                    Lifestyle
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Running`}>
                                    Running
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Basketball`}>
                                    Basketball
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Football`}>
                                    Football
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Gym & Training`}>
                                    Gym & Training
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Skateboarding`}>
                                    Skateboarding
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Tennis`}>
                                    Tennis
                                </Link>
                            </div>
                        </div>
                    </li>
                    <li className="item">
                        <a href>
                            GIÀY NỮ
                            <i className="fas fa-chevron-down" />
                        </a>
                        <div className="dropdownList">
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Lifestyle`}>
                                    Lifestyle
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Running`}>
                                    Running
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Basketball`}>
                                    Basketball
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Football`}>
                                    Football
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Gym & Training`}>
                                    Gym & Training
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Skateboarding`}>
                                    Skateboarding
                                </Link>
                            </div>
                            <div className="dropdownList_item">
                                <Link to={`/Sanpham_display/Tennis`}>
                                    Tennis
                                </Link>
                            </div>
                        </div>
                    </li>
                    <li className="item">
                        <a href>
                            LIÊN HỆ
                        </a>
                    </li>
                </ul>
            </div>
        );
    }

}

export default Navigation;