using ProMama.ViewModels.Home;
using ProMama.Views.Home;

namespace ProMama.Models
{
    class Aplicativo
    {
        private Aplicativo() { }

        private static Aplicativo _instance;
        public static Aplicativo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Aplicativo();
                }
                return _instance;
            }
        }

        public Usuario _usuario { get; set; }
        public Crianca _crianca { get; set; }
        public HomeMasterViewModel _master { get; set; }
        public Home _home { get; set; }
        public Sincronizacao _sync { get; set; }
        public bool _onThread { get; set; }
        public int _easterEggCount { get; set; }
    }
}