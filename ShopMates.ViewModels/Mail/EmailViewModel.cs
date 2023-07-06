using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MailKit.Net.Smtp;
using MimeKit;

namespace ShopMates.ViewModels.Mail
{
    public class EmailViewModel
    {
        [Required, Display(Name = "Your Name")]
        public string fromname { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string frommail { get; set; }
        [Required, Display(Name = "Client email"), EmailAddress]
        public string toemail { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string message { get; set; }
    }
}
