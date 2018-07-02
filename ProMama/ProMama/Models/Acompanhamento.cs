using ProMama.Components;
using System;

namespace ProMama.Models
{
    public class Acompanhamento
    {
        public int id { get; set; }
        public int crianca { get; set; }
        public DateTime data { get; set; }
        public string peso { get; set; }
        public string altura { get; set; }
        public string alimentacao { get; set; }
        public string idade { get; set; }
        public bool uploaded { get; set; }

        public Acompanhamento() { }

        public Acompanhamento(int _crianca, DateTime _data, string _peso, string _altura, string _alimentacao)
        {
            crianca = _crianca;
            data = _data;
            peso = _peso;
            altura = _altura;
            alimentacao = _alimentacao;
            uploaded = false;

            Aplicativo app = Aplicativo.Instance;
            idade = Ferramentas.DaysToFullString((_data - app._crianca.crianca_dataNascimento).Days, 2);
        }
    }
}
