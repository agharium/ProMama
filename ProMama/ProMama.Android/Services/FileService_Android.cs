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
        private readonly string FileUrlCrianca = "http://promama.cf/api/read-foto-crianca/";
        private readonly string FileUrlUser = "http://promama.cf/api/read-foto-user/";

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string DownloadFile(string url, string api_token, int type)
        {
            string localFilename = url;
            url = type == 0 ? FileUrlCrianca + url + "?api_token=" + api_token : FileUrlUser + url + "?api_token=" + api_token;

            var webClient = new WebClient();
            string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), localFilename);

            webClient.DownloadFile(new Uri(url), localPath);
            System.Diagnostics.Debug.WriteLine(localPath);
            return localPath;
        }
    }
}