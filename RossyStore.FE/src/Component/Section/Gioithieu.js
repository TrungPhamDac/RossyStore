import React, { Component } from 'react'
import Header from '../Header';
import Navigation from './Navigation';
import Footer from './Footer';

class Gioithieu extends Component {

    render() {
        return (
            <div>
                <Header></Header>
                <Navigation></Navigation>
                <div className="introduction grid">
                    <h3 className="title">GIỚI THIỆU</h3>
                    <div className="content">
                    Việt Nam là một trong năm quốc gia đứng đầu về nghành xuất khẩu giày dép lớn nhất thế giới, giá trị xuất khẩu trung bình lên tới 10 tỷ USD/năm. Đến nay nước ta hiện có gần 3000 doanh nghiệp hoạt động về nghành giày dép, thế nhưng thị trường giày dép " nội địa" lại bị bỏ quên khi các sản phẩm nhập khẩu chiếm 60% thị phần.Năm 2018 mức tiêu thụ sản phẩm giày dép tại nước ta khoảng 190 triệu đôi, dự báo sẽ tiếp tục tăng đến 355 triệu đôi vào năm 2020 vì chất lượng cuộc sống ngày càng tăng cao nên nhu cầu của sắm vì thế mà phát triển. Câu hỏi đặt đặt ra ở đây là "Tại sao các doanh nghiệp nước ngoài chọn Việt Nam làm đính để mở rộng và phát triển kinh doanh nhưng nước chủ nhà lại không thể phát triển mạnh sản phẩm nội địa". Việt Nam hiện nay có rất nhiều thương hiệu nổi tiếng ,nhưng chỉ có số ít trụ lại được cho đến hiện nay vì chính sách mở cửa kinh tế của chính phủ đã khiến các nghành bị cạnh tranh từ bên trong lẫn bên ngoài và nghành giày dép cũng vậy. Các doanh nghiệp hiểu rõ rằng nếu không nắm được những mong muốn, nhu cầu khách hàng thì sẽ đồng nghĩa với việc thất bại. Việc làm khách hàng hài lòng trở thành một vấn đề thiết yếu của doanh nghiệp, việc đó ảnh hưởng trực tiếp với sự tồn tại và phát triển của một công ty.Đó cũng là lý do mà thương hiệu Rossy Store được ra đời, Rossy Store muốn đưa sản phẩm giày dép do chính người Việt Nam chúng ta sản xuất và thiết kế được phát triển rộng rãi tại thị trường giày dép trong nước và quốc tế.
                    </div>
                    <div className="content">
                    Với phương châm “ Giày chuẩn Âu – Giá Ưu Việt” Rossy Store muốn truyền tải những thông điệp về hình ảnh cũng như chất lượng tốt nhất trên từng đôi giày. Chúng tôi không ngừng nổ lực để nâng cao chất lượng giày Việt đến cho mọi người và giá thành sản phẩm thật hợp lý . Với kinh nghiệm hơn 30 năm sản xuất và xuất khẩu hàng năm đội ngũ công nhân viên của nhà máy đã sản xuất ra thị trường quốc tế, nhất là những thị trường khó tính như Anh, Mỹ, Úc hơn 2 triệu đôi giày.Rossy Store áp dụng quy trình kỹ thuật công nghệ sản xuất  chặt chẽ đạt chuẩn châu Âu, từ khâu quản lý chuyên nghiệp, thiết kế kiểu dáng và đội ngũ hơn 2000 công nhân có kinh nghiệm làm giày lâu năm. Chúng tôi tự hào khi đem đến cho người tiêu dùng Việt những đôi giày cực kỳ chắc chắn, mềm mại, kĩ lưỡng từng đường may theo từng mm và luôn đạt chuẩn Quốc tế.
                    </div>
                </div>
                <Footer></Footer>
            </div>

        );
    }

}
export default Gioithieu;