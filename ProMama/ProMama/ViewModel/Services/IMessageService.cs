using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    interface IMessageService
    {
        Task ShowAsync(string message);
    }
}
