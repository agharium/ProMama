using ProMama.Droid.Services;
using ProMama.ViewModels.Services;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService_Android))]
namespace ProMama.Droid.Services
{
    class FileService_Android : IFileService
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}