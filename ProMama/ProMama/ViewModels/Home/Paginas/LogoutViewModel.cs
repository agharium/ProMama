using ProMama.Models;
using ProMama.ViewModels.Services;
using ProMama.Views.Home;
using System;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class LogoutViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private INavigation Navigation;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;

        public LogoutViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();

            Logout();
        }

        private async void Logout()
        {
            if (await _messageService.ConfirmationDialog("Você tem certeza que deseja sair?", "Sair", "Voltar"))
            {
                App.ConfigDatabase.Delete();

                app._crianca = null;
                app._usuario = null;

                _navigationService.NavigateCadastroLogin();
            }
            else
            {
                try
                {
                    App app = Application.Current as App;
                    MasterDetailPage md = (MasterDetailPage)app.MainPage;

                    var page = (Page)Activator.CreateInstance(typeof(HomeDetail));
                    md.Detail = new NavigationPage(page);
                    md.IsPresented = false;
                } catch (System.Reflection.TargetInvocationException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }
    }
}
