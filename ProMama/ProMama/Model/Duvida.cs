namespace ProMama.Model
{
    class Duvida
    {
        public string Pergunta { get; private set; }
        public string Resposta { get; private set; }

        public Duvida(string pergunta, string resposta)
        {
            Pergunta = pergunta;
            Resposta = resposta;
        }
    }
}
