using System;

namespace ProMama.Model
{
    class Medicao
    {
        public String Data { get; set; }
        public String Peso { get; set; }
        public String Altura { get; set; }
        public String Alimentacao { get; set; }

        public Medicao(string data, string peso, string altura, string alimentacao)
        {
            Data = data;
            Peso = peso;
            Altura = altura;
            Alimentacao = alimentacao;
        }
    }
}
