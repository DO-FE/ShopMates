using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.ViewModels.System.Users
{
    public class RegusterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegusterRequestValidator() 
        { 
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Điền Họ vào nhanh").MaximumLength(200).WithMessage("Nhập gì nhiều vậy? dưới 200 kí tự thôi");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Điền Tên vào nhanh").MaximumLength(200).WithMessage("Nhập gì nhiều vậy? dưới 200 kí tự thôi");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Sinh năm nào mà hơn 100 tuổi vậy ? Có xà lơ ko");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Điền Email vào nhanh").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Tính nhập bừa à? Nhập theo chuẩn email@email.com");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Điền SDT vào nhanh, để biết còn ket ban zalo");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Chỗ này đòi hỏi tên nè");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Chỗ này đòi mật khẩu nè ba má").MinimumLength(6).WithMessage("Nhập ít nhất là 7 kí tự và có 1 chữ Hoa và 1 kí tự đặt biệt nha");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Có Nhập pass lại cũng không khớp thì làm gì cho đời ?");
                }
            });
        }
    }
}
