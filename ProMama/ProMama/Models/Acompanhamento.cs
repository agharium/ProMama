using Newtonsoft.Json;

namespace ProMama.Models
{
    public class Acompanhamento
    {
        public int id { get; set; }
        public int crianca { get; set; }
        public string data { get; set; }
        public string peso { get; set; }
        public string altura { get; set; }
        public int alimentacao { get; set; }
        public bool uploaded { get; set; }

        [JsonIgnore]
        public string alimentacao_texto { get; set; }

        public Acompanhamento() { }

        public Acompanhamento(int _crianca, string _data, string _peso, string _altura, int _alimentacao)
        {
            crianca = _crianca;
            data = _data;
            peso = _peso;
            altura = _altura;
            alimentacao = _alimentacao;
            uploaded = false;
        }
    }
}
