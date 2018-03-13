using Newtonsoft.Json;
using System;

namespace ProMama.Model
{
    public class Crianca
    {
        public int              crianca_id { get; set; }
        public Usuario          crianca_usuario { get; set; }
        public string           crianca_primeiro_nome { get; set; }
        public string           crianca_sobrenome { get; set; }
        public DateTime         crianca_dataNascimento { get; set; }
        public int              crianca_sexo { get; set; }
        public double           crianca_pesoAoNascer { get; set; }
        public double           crianca_alturaAoNascer { get; set; }
        public string           crianca_outrasInformacoes { get; set; }
        public int              crianca_idade_gestacional { get; set; }
        public int              crianca_tipo_parto { get; set; }

        // variáveis auxiliares
        [JsonIgnore]
        public int IdadeSemanas { get; private set; }
        [JsonIgnore]
        public int IdadeMeses { get; private set; }
        [JsonIgnore]
        public string IdadeExtenso { get { return DefineIdadeExtenso(); } set { } }


        public Crianca(Usuario usuario, string primeiroNome, DateTime dataNascimento)
        {
            crianca_usuario = usuario;
            crianca_primeiro_nome = primeiroNome;
            crianca_dataNascimento = dataNascimento;

            IdadeSemanas = (DateTime.Now - dataNascimento).Days / 7;
            IdadeMeses = IdadeSemanas / 4;
        }

        public Crianca(string primeiroNome, int idadeSemanas)
        {
            crianca_primeiro_nome = primeiroNome;
            
            IdadeSemanas = idadeSemanas;
            IdadeMeses = idadeSemanas / 4;
        }

        public Crianca() { }

        public void CalculaIdadeAtual()
        {
            IdadeSemanas = (DateTime.Now - crianca_dataNascimento).Days / 7;
            IdadeMeses = IdadeSemanas / 4;
        }

        public string DefineIdadeExtenso()
        {
            if (IdadeMeses == 0)
            {
                return "2 semanas";
            }
            else if (IdadeMeses >= 12)
            {
                if (IdadeMeses == 12)
                {
                    return "1 ano";
                }
                else if (IdadeMeses >= 24)
                {
                    return "2 anos";
                }
                else
                {
                    if (IdadeMeses - 12 == 1)
                    {
                        return "1 ano e 1 mês";
                    }
                    else
                    {
                        return "1 ano e " + (IdadeMeses - 12) + " meses";
                    }
                }
            }
            else
            {
                if (IdadeMeses == 1)
                {
                    return "1 mês";
                }
                else
                {
                    return IdadeMeses + " meses";
                }
            }
        }
    }
}