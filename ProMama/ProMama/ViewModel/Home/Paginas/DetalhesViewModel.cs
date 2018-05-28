using ProMama.Model;

namespace ProMama.ViewModel.Home.Paginas
{
    class DetalhesViewModel : ViewModelBase
    {
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Texto { get; set; }

        public DetalhesViewModel(Informacao i)
        {
            Titulo = i.informacao_titulo;
            Imagem = i.informacao_imagem;
            Texto = i.informacao_corpo;
        }

        public DetalhesViewModel(Duvida d)
        {
            Titulo = d.duvida_pergunta;
            Texto = d.duvida_resposta;
        }
    }
}
