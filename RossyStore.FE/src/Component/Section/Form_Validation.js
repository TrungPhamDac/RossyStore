

export default function Validator(options) {

    function ruleExecute_onBlur(inputElement, rule)
    {
        var errorElement = inputElement.parentElement.querySelector('.message');
        var errorMessage = rule.test(inputElement.value);
        if (errorMessage){
            errorElement.innerText = errorMessage; /* gán nội dung cho thẻ thông báo lỗi */
            inputElement.parentElement.classList.add('error');
        }
        else  {
            errorElement.innerText ='';
            inputElement.parentElement.classList.remove('error');
        }
    }
    
    function ruleExecute_onInput(inputElement)
    {
        var errorElement = inputElement.parentElement.querySelector('.message');
        errorElement.innerText ='';
        inputElement.parentElement.classList.remove('error');
     
    }

    var formElement = document.querySelector('#signUp-form'); /* lấy ra form cần thao tác */ 
    if (formElement){ /* nếu lấy thành công thì ... */
        options.rules.forEach(function (rule){ /* duyệt mảng rules trong Vali đã tạo bên file html */
            var inputElement = formElement.querySelector(rule.selector); /* tạo mảng chứa các đối tượng input*/
             /* Lấy ra thông báo lỗi bằng cách : thẻInput.thẻCha.thẻMessange */
            if (inputElement) /* nếu lấy thành công thì ... */{
                inputElement.onblur = function() // xử lí khi blur ra ngoài
                {
                    ruleExecute_onBlur(inputElement, rule);
                }
                inputElement.oninput = function()
                {
                    ruleExecute_onInput(inputElement, rule);
                }
            }
        })
    }
    
    const isRequired = selector => {
        return {
            selector: selector,
            test: function (value) {
                if (value.trim())
                    return undefined ;
                else return 'Vui lòng nhập trường này';
            }
        };
    }
    
    
    const isEmail = selector => {
        return{
            selector: selector,
            test: function (value) {
                var regex = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                return regex.test(value) ? undefined : 'Email không hợp lệ';
            }
        };
    }
    
    
    const isPassword = selector  =>  {
        return {
            selector: selector,
            test: function (value) {
                var Min = 6;
                if (value.trim() && value.length >=6)
                    return undefined ;
                else return 'Độ dài mật khẩu phải lớn hơn 6 kí tự';
            }
        };
    }
    
    const PasswordConfirm = (selector, getPassword)  =>  {
        return{
            selector: selector,
            test: function(value){
                return value == getPassword() ? undefined : 'Mật khẩu không khớp';
            }
        }
    }

}

