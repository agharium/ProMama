using Newtonsoft.Json;

namespace ProMama.Models
{
    public class Conversa
    {
        public int id { get; set; }
        public int user { get; set; }
        public string pergunta { get; set; }
        public string resposta { get; set; }
        public int paraTodos { get; set; }
        public string resumo { get; set; }

        public Conversa(int _id, string _pergunta, string _resposta)
        {
            id = _id;
            pergunta = _pergunta;
            resposta = _resposta;
            resumo = _resposta;
        }

        public Conversa() { }
    }
}
