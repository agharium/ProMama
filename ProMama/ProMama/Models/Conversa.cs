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

        public Conversa(int _id, int _user, string _pergunta, string _resposta)
        {
            id = _id;
            user = _user;
            pergunta = _pergunta;
            resposta = _resposta;
            resumo = _resposta;
        }

        public Conversa() { }
    }
}
