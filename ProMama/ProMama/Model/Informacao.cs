using Newtonsoft.Json;
using System;

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
    }
}
