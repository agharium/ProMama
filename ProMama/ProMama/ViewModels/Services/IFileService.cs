namespace ProMama.ViewModels.Services
{
    public interface IFileService
    {
        byte[] ReadAllBytes(string path);

        string DownloadFile(string url, string api_token, int type);
        // type: 0 = crianca - 1 = user
    }
}
