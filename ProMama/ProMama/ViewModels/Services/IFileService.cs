namespace ProMama.ViewModels.Services
{
    public interface IFileService
    {
        byte[] ReadAllBytes(string path);

        string DownloadFile(string url, string api_token);
    }
}
