using ProMama.Data;
using ProMama.Model;
using Xamarin.Forms;

namespace ProMama
{
    public partial class App : Application
    {
        static MarcelloDB.Session _db;
        static ConfigDatabaseController _configDatabase;
        static UsuarioDatabaseController _usuarioDatabase;
        static CriancaDatabaseController _criancaDatabase;
        static BairroDatabaseController _bairroDatabase;
        static PostoDatabaseController _postoDatabase;
        private Aplicativo app = Aplicativo.Instance;

        public App()
        {
            // Iconize
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            DependencyService.Register<ViewModel.Services.INavigationService, View.Services.NavigationService>();
            DependencyService.Register<ViewModel.Services.IMessageService, View.Services.MessageService>();
            DependencyService.Register<ViewModel.Services.IRestService, View.Services.RestService>();

            InitializeComponent();

            // verifica se usuário já está logado
            var cfg = ConfigDatabase.FindConfig();
            if (ConfigDatabase.FindConfig() != null && cfg.config_usuario != null && cfg.config_crianca != null)
            {
                app._usuario = cfg.config_usuario;
                app._crianca = cfg.config_crianca;
                MainPage = new View.Home.Home();
            }
            else
            {
                MainPage = new View.Inicio.IntroducaoView();
            }
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

        public static MarcelloDB.Session DB
        {
            get
            {
                if (_db == null)
                {
                    _db = DependencyService.Get<IMarcelloDB>().GetSession();
                }
                return _db;
            }
        }

        public static ConfigDatabaseController ConfigDatabase
        {
            get
            {
                if (_configDatabase == null)
                {
                    _configDatabase = new ConfigDatabaseController();
                }
                return _configDatabase;
            }
        }

        public static UsuarioDatabaseController UsuarioDatabase
        {
            get
            {
                if (_usuarioDatabase == null)
                {
                    _usuarioDatabase = new UsuarioDatabaseController();
                }
                return _usuarioDatabase;
            }
        }

        public static CriancaDatabaseController CriancaDatabase
        {
            get
            {
                if (_criancaDatabase == null)
                {
                    _criancaDatabase = new CriancaDatabaseController();
                }
                return _criancaDatabase;
            }
        }

        public static BairroDatabaseController BairroDatabase
        {
            get
            {
                if (_bairroDatabase == null)
                {
                    _bairroDatabase = new BairroDatabaseController();
                }
                return _bairroDatabase;
            }
        }

        public static PostoDatabaseController PostoDatabase
        {
            get
            {
                if (_postoDatabase == null)
                {
                    _postoDatabase = new PostoDatabaseController();
                }
                return _postoDatabase;
            }
        }
    }
}
