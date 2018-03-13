namespace ProMama.Model
{
    public class Bairro {
        public int bairro_id { get; set; }
        public string bairro_nome { get; set; }

        public Bairro() {}

        public Bairro(string bairro_nome)
        {
            this.bairro_nome = bairro_nome;
        }
    }
}
