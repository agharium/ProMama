namespace ProMama.ViewModel.Services
{
    public interface INotificationService
    {
        void Notify(string titulo, string texto, int dias);
    }
}
