namespace ProMama.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
