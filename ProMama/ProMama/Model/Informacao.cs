using System.Text.RegularExpressions;

namespace ProMama.Model
{
    public class Informacao
    {
        public int IdadeInicio { get; private set; }
        public int IdadeFim { get; private set; }
        public string Titulo { get; private set; }
        public string Texto { get; private set; }
        public string Resumo { get; private set; }
        public string Imagem { get; private set; }

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
