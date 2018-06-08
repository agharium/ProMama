namespace ProMama.Models
{
    public class Posto
    {
        public int posto_id { get; set; }
        public string posto_nome { get; set; }
        public string posto_endereco { get; set; }
        public string posto_telefone { get; set; }
        public int posto_bairro { get; set; }

        public Posto() {}
    }
}
