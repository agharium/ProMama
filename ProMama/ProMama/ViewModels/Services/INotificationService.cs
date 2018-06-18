namespace ProMama.ViewModels.Services
{
    public interface INotificationService
    {
        void Notify(int id, string titulo, string texto, int dias);

        void Cancel(int id);

        void CancelAll();
    }
}
