using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
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
                App.ConfigDatabase.DeleteConfig();

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

                    var page = (Page)Activator.CreateInstance(typeof(View.Home.HomeDetail));
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
