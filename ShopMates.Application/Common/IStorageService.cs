using ShopMates.Data.Entities;
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
        string GetFileUrl(string fileName);

        Task SaveFileAsync (Stream mediaBinaryStream, string fileName);

        Task<string> LoadFileAsync(string fileName);

        Task DeleteFileAsync(string fileName);

        Task<FileStream> GetImageFile(string imagePath);
    }
}
