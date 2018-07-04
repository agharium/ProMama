using ProMama.Droid.Services;
using ProMama.ViewModels.Services;
using System;
using System.IO;
using System.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService_Android))]
namespace ProMama.Droid.Services
{
    class FileService_Android : IFileService
    {
        private readonly string FileUrl = "http://promama.cf/api/file/";

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string DownloadFile(string url, string api_token)
        {
            string localFilename = url;
            url = FileUrl + url + "?api_token=" + api_token;

            var webClient = new WebClient();
            string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), localFilename);

            webClient.DownloadFile(new Uri(url), localPath);
            System.Diagnostics.Debug.WriteLine(localPath);
            return localPath;
        }
    }
}