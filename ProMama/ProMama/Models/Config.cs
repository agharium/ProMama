namespace ProMama.Models
{
    public class Config
    {
        public int config_id { get { return 1; } set { } }
        public Usuario config_usuario { get; set; }
        public Crianca config_crianca { get; set; }

        public Config(Usuario usuario, Crianca crianca)
        {
            config_usuario = usuario;
            config_crianca = crianca;
        }

        public Config() {}
    }
}
