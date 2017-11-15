using System;

namespace ProMama.Model
{
    class Medicao
    {
        public String Data { get; set; }
        public String Peso { get; set; }
        public String Altura { get; set; }
        public String Alimentacao { get; set; }

        public Medicao(String Data, String Peso, String Altura, String Alimentacao)
        {
            this.Data = Data;
            this.Peso = Peso;
            this.Altura = Altura;
            this.Alimentacao = Alimentacao;
        }
    }
}
