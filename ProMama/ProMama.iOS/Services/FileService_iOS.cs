using ProMama.iOS.Services;
using ProMama.ViewModels.Services;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService_iOS))]
namespace ProMama.iOS.Services
{
    class FileService_iOS : IFileService
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}