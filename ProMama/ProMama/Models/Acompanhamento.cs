using Newtonsoft.Json;
using ProMama.Components;
using System;

namespace ProMama.Models
{
    public class Acompanhamento
    {
        public int id { get; set; }
        public int crianca { get; set; }
        public DateTime data { get; set; }
        public string dataPorExtenso { get; set; }
        public int peso { get; set; }
        public int altura { get; set; }
        public string alimentacao { get; set; }
        public bool uploaded { get; set; }

        [JsonIgnore]
        public string pesoExtenso { get; set; }
        [JsonIgnore]
        public string alturaExtenso { get; set; }

        public Acompanhamento() { }

        public Acompanhamento(int _crianca, DateTime _data, int _peso, int _altura, string _alimentacao)
        {
            crianca = _crianca;
            data = _data;
            peso = _peso;
            altura = _altura;
            alimentacao = _alimentacao;
            uploaded = false;

            Aplicativo app = Aplicativo.Instance;
            dataPorExtenso = Ferramentas.DaysToFullString((_data - app._crianca.crianca_dataNascimento).Days, 2);
        }
    }
}
