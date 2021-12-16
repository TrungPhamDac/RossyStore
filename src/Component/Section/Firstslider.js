import React, { Component } from 'react'

import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

class Firstslider extends Component {
    render() {
        const settings = {
            dots: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 2000,
            outline: false
        };
        return (
            <div className="slideShow1 grid">
                <Slider {...settings}>
                    <div>
                        <img src="/img/slide_index_1.jpg" alt="" />
                    </div>
                    <div>
                        <img src="/img/slide_index_2.jpg" alt="" />
                    </div>

                </Slider>
            </div>


        );
    }

}

export default Firstslider;