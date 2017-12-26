using System;

namespace ProMama.Model
{
    public class Crianca
    {
        public int Id { get; private set; }
        public Usuario Mae { get; private set; }
        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Sexo { get; private set; }
        public double PesoAoNascer { get; private set; }
        public double AlturaAoNascer { get; private set; }
        public string OutrasInformacoes { get; private set; }
        public int IdadeGestacional { get; private set; }
        public int TipoParto { get; private set; }

        // variáveis auxiliares
        public int IdadeSemanas { get; private set; }
        public int IdadeMeses { get; private set; }

        public Crianca(Usuario mae, string primeiroNome, DateTime dataNascimento)
        {
            Mae = mae;
            PrimeiroNome = primeiroNome;
            DataNascimento = dataNascimento;

            IdadeSemanas = (DateTime.Now - DataNascimento).Days / 7;
            IdadeMeses = IdadeSemanas / 4;
        }

        public Crianca(string primeiroNome, int idadeSemanas)
        {
            PrimeiroNome = primeiroNome;

            IdadeSemanas = idadeSemanas;
            IdadeMeses = idadeSemanas / 4;
        }

        public void CalculaIdadeAtual()
        {
            IdadeSemanas = (DateTime.Now - DataNascimento).Days / 7;
            IdadeMeses = IdadeSemanas / 4;
        }
    }
}
