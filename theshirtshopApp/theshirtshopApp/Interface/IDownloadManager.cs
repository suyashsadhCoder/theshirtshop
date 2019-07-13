using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.Interface
{
    public interface IDownloadManager
    {
        Task<string> SaveFiles(string filename, byte[] bytes);
        void OpenFile(string filePath, string filename);
         void Main(string uri);
    }
}
