namespace ProMama.ViewModels.Services
{
    public interface INotificationService
    {
        void Notify(string titulo, string texto, int dias);
    }
}
