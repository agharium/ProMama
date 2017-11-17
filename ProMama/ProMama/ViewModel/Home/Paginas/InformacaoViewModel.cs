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
            this.Titulo = info.Titulo;
            this.Imagem = info.Imagem;
            this.Texto = info.Texto;
        }
    }
}
