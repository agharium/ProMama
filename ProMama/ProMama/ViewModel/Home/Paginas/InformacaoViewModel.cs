using ProMama.Model;

namespace ProMama.ViewModel.Home.Paginas
{
    class InformacaoViewModel : ViewModelBase
    {
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Texto { get; set; }

        public InformacaoViewModel(Informacao info)
        {
            this.Titulo = info.informacao_titulo;
            this.Imagem = info.informacao_imagem;
            this.Texto = info.informacao_corpo;
        }
    }
}
