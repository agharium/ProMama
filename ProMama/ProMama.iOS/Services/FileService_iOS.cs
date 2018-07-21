using ProMama.iOS.Services;
using ProMama.ViewModels.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService_iOS))]
namespace ProMama.iOS.Services
{
    class FileService_iOS : IFileService
    {
        private readonly string FileUrlCrianca = "http://promama.cf/api/foto-crianca/";
        private readonly string FileUrlUser = "http://promama.cf/api/foto-user/";

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string DownloadFile(string url, string api_token, int type)
        {
            string localFilename = url.Substring(url.LastIndexOf('/') + 1);
            url = type == 0 ? FileUrlCrianca + url + "?api_token=" + api_token : FileUrlUser + url + "?api_token=" + api_token;

            var webClient = new WebClient();
            string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), localFilename);

            try
            {
                webClient.DownloadFile(new Uri(url), localPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            Debug.WriteLine(localPath);
            return localPath;
        }
    }
}