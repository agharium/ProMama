using System;
using System.Collections.Generic;

namespace ProMama.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public int bairro { get; set; }
        public int posto_saude { get; set; }
        public DateTime data_nascimento { get; set; }
        public List<int> criancas { get; set; }
        public string api_token { get; set; }
        public List<int> notificacoes_oQuantoAntes { get; set; }
        public string senha_reserva { get; set; }
        public bool uploaded { get; set; }
        public string foto_url { get; set; }
        public string foto_caminho { get; set; }
        public bool foto_uploaded { get; set; }

        public Usuario(string _email, string _password, int _bairro)
        {
            email = _email;
            password = _password;
            bairro = _bairro;
            posto_saude = -1;
            criancas = new List<int>();
            uploaded = true;
            foto_caminho = "avatar_default.jpg";
            foto_uploaded = true;
        }

        public Usuario(string _email, string _password)
        {
            email = _email;
            password = _password;
        }

        public Usuario() {}
    }
}
