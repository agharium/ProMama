using System;
using System.Collections.Generic;

namespace ProMama.Models
{
    public class Usuario
    {
        public int           id              { get; set; }
        public string        email           { get; set; }
        public string        password        { get; set; }
        public string        name            { get; set; }
        public int           bairro          { get; set; }
        public int           posto_saude     { get; set; }
        public DateTime      data_nascimento { get; set; }
        public List<Crianca> criancas        { get; set; }
        public string        api_token       { get; set; }

        public Usuario(string _email, string _password, int _bairro)
        {
            email = _email;
            password = _password;
            bairro = _bairro;
        }

        public Usuario(string _email, string _password)
        {
            email = _email;
            password = _password;
        }

        public Usuario() {}
    }
}
