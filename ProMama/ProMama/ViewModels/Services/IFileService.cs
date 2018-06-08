namespace ProMama.ViewModels.Services
{
    public interface IFileService
    {
        byte[] ReadAllBytes(string path);
    }
}
