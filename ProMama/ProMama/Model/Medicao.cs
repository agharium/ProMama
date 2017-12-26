namespace ProMama.Model
{
    class Medicao
    {
        public string Data { get; private set; }
        public string Peso { get; private set; }
        public string Altura { get; private set; }
        public string Alimentacao { get; private set; }

        public Medicao(string data, string peso, string altura, string alimentacao)
        {
            Data = data;
            Peso = peso;
            Altura = altura;
            Alimentacao = alimentacao;
        }
    }
}
