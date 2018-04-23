using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace ProMama.Model
{
    public class Informacao
    {
        public int informacao_id { get; set; }
        public string informacao_titulo { get; set; }
        public string informacao_corpo { get; set; }
        public DateTime informacao_data { get; set; }
        public string informacao_autor { get; set; }
        public int informacao_idadeSemanasInicio { get; set; }
        public int informacao_idadeSemanasFim { get; set; }

        [JsonIgnore]
        public string informacao_resumo { get; set; }
        [JsonIgnore]
        public string informacao_imagem { get; set; }
        [JsonIgnore]
        public int informacao_imagem_altura { get; set; }

        public Informacao() { }

        public Informacao(int informacao_idadeSemanasInicio, int informacao_idadeSemanasFim, string informacao_titulo, string informacao_corpo, string informacao_imagem)
        {
            this.informacao_titulo = informacao_titulo;
            this.informacao_corpo = informacao_corpo;
            this.informacao_idadeSemanasInicio = informacao_idadeSemanasInicio;
            this.informacao_idadeSemanasFim = informacao_idadeSemanasFim;
            this.informacao_resumo = Regex.Match(informacao_corpo, @"^(\w+\b.*?){20}").ToString() + "...";
            this.informacao_imagem = informacao_imagem;
        }
    }
}
