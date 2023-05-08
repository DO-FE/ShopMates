using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.ViewModels.System.Users
{
    public class UserDeleteRequest
    {
        [Display(Name = "ID User")]
        public Guid Id { get; set; }
    }
}
