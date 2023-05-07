using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ShopMates.ViewModels.Common
{
	public class APISuccessResult<T> : APIResult<T>
	{
		public APISuccessResult(T resultObj)
		{
			IsSuccessed = true;
			ResultObj = resultObj;
		}

		public APISuccessResult()
		{
			IsSuccessed = true;
		}
	}
}
