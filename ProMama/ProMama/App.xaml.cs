using Xamarin.Forms;

namespace ProMama
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<ViewModel.Services.INavigationService, View.Services.NavigationService>();
            DependencyService.Register<ViewModel.Services.IMessageService, View.Services.MessageService>();
            DependencyService.Register<ViewModel.Services.IRestService, View.Services.RestService>();

            InitializeComponent();

            MainPage = new View.Inicio.IntroducaoView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
