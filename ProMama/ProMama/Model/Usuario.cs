using System.Collections.Generic;

namespace ProMama.Model
{
    public class Usuario
    {
        public int usuario_id { get; set; }
        public string usuario_email { get; set; }
        public string usuario_senha { get; set; }
        public string usuario_nome { get; set; }
        public Bairro usuario_bairro { get; set; }
        public Posto usuario_postoSaude { get; set; }
        public string usuario_dataNascimento { get; set; }
        public List<Crianca> usuario_criancas { get; set; }

        public Usuario(int usuario_id, string usuario_email, string usuario_senha, string usuario_nome, Bairro usuario_bairro, Posto usuario_postoSaude, string usuario_dataNascimento)
        {
            this.usuario_id = usuario_id;
            this.usuario_email = usuario_email;
            this.usuario_senha = usuario_senha;
            this.usuario_nome = usuario_nome;
            this.usuario_bairro = usuario_bairro;
            this.usuario_postoSaude = usuario_postoSaude;
            this.usuario_dataNascimento = usuario_dataNascimento;
        }

        public Usuario(string usuario_email, string usuario_senha)
        {
            this.usuario_email = usuario_email;
            this.usuario_senha = usuario_senha;
        }

        public Usuario() {}
    }
}
