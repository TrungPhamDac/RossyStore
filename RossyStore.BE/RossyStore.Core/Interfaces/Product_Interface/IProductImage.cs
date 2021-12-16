using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Product_Interface
{
    interface IProductImage
    {
        Task<string> UploadToStorage(string Product_ID, FileStream stream, string Filename);
        void AddImages(string Product_ID, string Filename, string Link);
        string GetImage(string Product_ID, string FileName);
    }
}
