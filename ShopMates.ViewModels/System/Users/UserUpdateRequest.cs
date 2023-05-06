using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopMates.ViewModels.System.Users
{
	public class UserUpdateRequest
	{
		public Guid Id { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Display(Name = "Date of Birth")]
		[DataType(DataType.Date)]
		public DateTime Dob { get; set; }
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; }
	}
}
