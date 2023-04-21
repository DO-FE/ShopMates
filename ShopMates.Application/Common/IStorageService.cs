using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Common
{
    public interface IStorageService
    {
        String GetFileUrl(String fileName);

        Task SaveFileAsync (Stream mediaBinaryStream, String fileName);

        Task DeleteFileAsync(string fileName);
    }
}
