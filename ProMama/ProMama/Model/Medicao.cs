namespace ProMama.Model
{
    class Medicao
    {
        public string Data { get; set; }
        public string Peso { get; set; }
        public string Altura { get; set; }
        public string Alimentacao { get; set; }

        public Medicao(string data, string peso, string altura, string alimentacao)
        {
            Data = data;
            Peso = peso;
            Altura = altura;
            Alimentacao = alimentacao;
        }
    }
}
