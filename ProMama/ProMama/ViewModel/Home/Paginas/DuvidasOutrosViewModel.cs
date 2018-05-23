using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class DuvidasOutrosViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _indicadorLoading;
        public string IndicadorLoading
        {
            get
            {
                return _indicadorLoading;
            }
            set
            {
                _indicadorLoading = value;
                Notify("IndicadorLoading");
            }
        }

        private string _avisoListaVazia;
        public string AvisoListaVazia
        {
            get
            {
                return _avisoListaVazia;
            }
            set
            {
                _avisoListaVazia = value;
                Notify("AvisoListaVazia");
            }
        }

        private List<Duvida> DuvidasAux { get; set; }

        private ObservableCollection<Duvida> _duvidas;
        public ObservableCollection<Duvida> Duvidas
        {
            get { return _duvidas; }
            set
            {
                _duvidas = value;
                Notify("Duvidas");
            }
        }

        public ICommand BuscarCommand { get; set; }

        private readonly IRestService RestService;

        public DuvidasOutrosViewModel()
        {
            RestService = DependencyService.Get<IRestService>();

            BuscarCommand = new Command<string>(Buscar);
            
            DuvidasRead();
        }

        public async void DuvidasRead()
        {
            IndicadorLoading = "True";
            AvisoListaVazia = "False";

            Duvidas = new ObservableCollection<Duvida>(await RestService.DuvidasRead(app._usuario.api_token));

            IndicadorLoading = "False";
            if (Duvidas.Count == 0)
                AvisoListaVazia = "True";

            DuvidasAux = new List<Duvida>(Duvidas);
        }

        public void Buscar(string termo)
        {
            Duvidas = string.IsNullOrEmpty(termo) ? new ObservableCollection<Duvida>(DuvidasAux) : new ObservableCollection<Duvida>(DuvidasAux.Where(d => d.duvida_pergunta.Contains(termo)));
            AvisoListaVazia = Duvidas.Count == 0 ? "True" : "False";
        }
    }
}
