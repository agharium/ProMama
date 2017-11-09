using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.View.Services
{
    class MessageService : ViewModel.Services.IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", message, "Voltar");
        }
    }
}
