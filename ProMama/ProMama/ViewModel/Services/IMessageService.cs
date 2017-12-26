using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    interface IMessageService
    {
        Task AlertDialog(string message);

        Task<bool> ConfirmationDialog(string message);
    }
}
