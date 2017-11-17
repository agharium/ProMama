using System.Text.RegularExpressions;

namespace ProMama.Model
{
    class Informacao
    {
        public int IdadeInicio { get; set; }
        public int IdadeFim { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Resumo { get; set; }
        public string Imagem { get; set; }

        public Informacao(int idadeInicio, int idadeFim, string titulo, string texto, string imagem)
        {
            IdadeInicio = idadeInicio;
            IdadeFim = idadeFim;
            Titulo = titulo;
            Texto = texto;
            Resumo = Regex.Match(texto, @"^(\w+\b.*?){20}").ToString() + "...";
            Imagem = imagem;
        }
    }
}
