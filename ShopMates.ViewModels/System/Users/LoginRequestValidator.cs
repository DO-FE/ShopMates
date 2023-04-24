using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.ViewModels.System.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() 
        { 
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Chỗ này đòi hỏi tên nè");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Chỗ này đòi mật khẩu nè ba má");
        }
    }
}
