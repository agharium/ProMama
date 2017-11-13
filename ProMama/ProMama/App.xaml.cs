using Xamarin.Forms;

namespace ProMama
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<ViewModel.Services.INavigationService, View.Services.NavigationService>();
            DependencyService.Register<ViewModel.Services.IMessageService, View.Services.MessageService>();

            InitializeComponent();

            // original:
            //MainPage = new View.Inicio.IntroducaoView();
            // testes:
            MainPage = new View.Home.Home();
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
