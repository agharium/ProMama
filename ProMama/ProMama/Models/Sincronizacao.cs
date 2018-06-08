namespace ProMama.Models
{
    public class Sincronizacao
    {
        public int id { get; set; }
        public int informacao { get; set; }
        public int notificacao { get; set; }
        public int bairro { get; set; }
        public int posto { get; set; }
        public int duvidas { get; set; }

        public Sincronizacao() { }

        public Sincronizacao(int _id)
        {
            id = _id;
            informacao = 0;
            notificacao = 0;
            bairro = 0;
            posto = 0;
            duvidas = 0;
        }
    }
}
