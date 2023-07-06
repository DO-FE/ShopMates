using ShopMates.ViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Mail
{
    public interface IEmailService
    {
        Task<int> SendMail(EmailViewModel request);
    }
}
