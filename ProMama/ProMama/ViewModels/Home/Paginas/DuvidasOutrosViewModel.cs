using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
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

        private INavigation Navigation { get; set; }

        private readonly IRestService RestService;
        private readonly INavigationService NavigationService;

        public ICommand BuscarCommand { get; set; }
        public ICommand AbrirDuvidaCommand { get; set; }

        public DuvidasOutrosViewModel(INavigation _navigation)
        {
            Navigation = _navigation;

            RestService = DependencyService.Get<IRestService>();
            NavigationService = DependencyService.Get<INavigationService>();

            BuscarCommand = new Command<string>(Buscar);
            AbrirDuvidaCommand = new Command<Duvida>(AbrirDuvida);

            DuvidasRead();
        }

        private async void DuvidasRead()
        {
            IndicadorLoading = "True";
            AvisoListaVazia = "False";

            Duvidas = new ObservableCollection<Duvida>(await RestService.DuvidasRead(app._usuario.api_token));
            foreach (var d in Duvidas)
            {
                d.duvida_resumo = String.Join(" ", d.duvida_resposta.Split().Take(20).ToArray());
                d.duvida_resumo.Remove(d.duvida_resumo.Length - 1, 1);
                d.duvida_resumo += "...";
            }

            IndicadorLoading = "False";
            if (Duvidas.Count == 0)
                AvisoListaVazia = "True";

            DuvidasAux = new List<Duvida>(Duvidas);
        }

        private void Buscar(string termo)
        {
            Duvidas = string.IsNullOrEmpty(termo) ? new ObservableCollection<Duvida>(DuvidasAux) : new ObservableCollection<Duvida>(DuvidasAux.Where(d => d.duvida_pergunta.Contains(termo)));
            AvisoListaVazia = Duvidas.Count == 0 ? "True" : "False";
        }

        private async void AbrirDuvida(Duvida duvida)
        {
            await NavigationService.NavigateDuvida(Navigation, duvida);
        }
    }
}
