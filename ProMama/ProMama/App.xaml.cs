using DLToolkit.Forms.Controls;
using MarcelloDB;
using Plugin.Iconize;
using ProMama.Database;
using ProMama.Database.Controllers;
using ProMama.Models;
using ProMama.Views.Home;
using ProMama.Views.Inicio;
using ProMama.Views.Services;
using ProMama.ViewModels.Services;
using Xamarin.Forms;
using Plugin.Settings;
using ProMama.Views.Home.Paginas;

namespace ProMama
{
    public partial class App : Application
    {
        static Session _db;
        
        static UsuarioDatabaseController _usuarioDatabase;
        static CriancaDatabaseController _criancaDatabase;
        static BairroDatabaseController _bairroDatabase;
        static PostoDatabaseController _postoDatabase;
        static ConversaDatabaseController _conversaDatabase;
        static InformacaoDatabaseController _informacaoDatabase;
        static SincronizacaoDatabaseController _sincronizacaoDatabase;
        static NotificacaoDatabaseController _notificacaoDatabase;
        static FotoDatabaseController _fotoDatabase;
        static AcompanhamentoDatabaseController _acompanhamentoDatabase;
        static MarcoDatabaseController _marcoDatabase;
        static DuvidaFrequenteDatabaseController _duvidaFrequenteDatabase;
        static ExcluirDatabaseController _excluirDatabase;

        private Aplicativo app = Aplicativo.Instance;

        public App()
        {
            // Iconize
            Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<IRestService, RestService>();

            InitializeComponent();

            // FlowListView
            FlowListView.Init();

            // verifica se usuário já está logado
            app._sync = SincronizacaoDatabase.Find();
            app._onThread = false;

            if (UltimoUsuario != 0)
            {
                app._usuario = UsuarioDatabase.Find(UltimoUsuario);
                if (UltimaCrianca != 0){
                    app._crianca = CriancaDatabase.Find(UltimaCrianca);
                    MainPage = new Home();
                }
                else
                {
                    app._crianca = null;
                    MainPage = new NavigationPage(new AddCriancaView());
                }                
            }
            else
            {
                MainPage = new IntroducaoView();
            }
        }

        public static int UltimoUsuario
        {
            get => CrossSettings.Current.GetValueOrDefault(nameof(UltimoUsuario), 0);
            set => CrossSettings.Current.AddOrUpdateValue(nameof(UltimoUsuario), value);
        }

        public static int UltimaCrianca
        {
            get => CrossSettings.Current.GetValueOrDefault(nameof(UltimaCrianca), 0);
            set => CrossSettings.Current.AddOrUpdateValue(nameof(UltimaCrianca), value);
        }

        private static Excluir _excluir;
        public static Excluir Excluir
        {
            get
            {
                if (_excluir == null)
                {
                    var del = ExcluirDatabase.Find();
                    _excluir = del ?? new Excluir(1);
                }
                return _excluir;
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

        public static Session DB
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

        public static ConversaDatabaseController ConversaDatabase
        {
            get
            {
                if (_conversaDatabase == null)
                {
                    _conversaDatabase = new ConversaDatabaseController();
                }
                return _conversaDatabase;
            }
        }

        public static InformacaoDatabaseController InformacaoDatabase
        {
            get
            {
                if (_informacaoDatabase == null)
                {
                    _informacaoDatabase = new InformacaoDatabaseController();
                }
                return _informacaoDatabase;
            }
        }

        public static SincronizacaoDatabaseController SincronizacaoDatabase
        {
            get
            {
                if (_sincronizacaoDatabase == null)
                {
                    _sincronizacaoDatabase = new SincronizacaoDatabaseController();
                }
                return _sincronizacaoDatabase;
            }
        }

        public static NotificacaoDatabaseController NotificacaoDatabase
        {
            get
            {
                if (_notificacaoDatabase == null)
                {
                    _notificacaoDatabase = new NotificacaoDatabaseController();
                }
                return _notificacaoDatabase;
            }
        }

        public static FotoDatabaseController FotoDatabase
        {
            get
            {
                if (_fotoDatabase == null)
                {
                    _fotoDatabase = new FotoDatabaseController();
                }
                return _fotoDatabase;
            }
        }

        public static AcompanhamentoDatabaseController AcompanhamentoDatabase
        {
            get
            {
                if (_acompanhamentoDatabase == null)
                {
                    _acompanhamentoDatabase = new AcompanhamentoDatabaseController();
                }
                return _acompanhamentoDatabase;
            }
        }

        public static MarcoDatabaseController MarcoDatabase
        {
            get
            {
                if (_marcoDatabase == null)
                {
                    _marcoDatabase = new MarcoDatabaseController();
                }
                return _marcoDatabase;
            }
        }

        public static DuvidaFrequenteDatabaseController DuvidaFrequenteDatabase
        {
            get
            {
                if (_duvidaFrequenteDatabase == null)
                {
                    _duvidaFrequenteDatabase = new DuvidaFrequenteDatabaseController();
                }
                return _duvidaFrequenteDatabase;
            }
        }

        public static ExcluirDatabaseController ExcluirDatabase
        {
            get
            {
                if (_excluirDatabase == null)
                {
                    _excluirDatabase = new ExcluirDatabaseController();
                }
                return _excluirDatabase;
            }
        }
    }
}
