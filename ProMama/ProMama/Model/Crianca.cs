using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace ProMama.Model
{
    public class Crianca
    {
        public int      crianca_id { get; set; }
        public string   crianca_primeiro_nome { get; set; }
        public string   crianca_sobrenome { get; set; }
        public DateTime crianca_dataNascimento { get; set; }
        public int      crianca_sexo { get; set; }
        public double   crianca_pesoAoNascer { get; set; }
        public double   crianca_alturaAoNascer { get; set; }
        public string   crianca_outrasInformacoes { get; set; }
        public int      crianca_idade_gestacional { get; set; }
        public int      crianca_tipo_parto { get; set; }

        // variáveis auxiliares
        [JsonIgnore]
        public double IdadeSemanas { get { return (DateTime.Now - crianca_dataNascimento).Days * 0.1551871428571429; } set { } }
        [JsonIgnore]
        public double IdadeMeses { get { return (DateTime.Now - crianca_dataNascimento).Days / 30.4167; } set { } }
        [JsonIgnore]
        public string IdadeExtenso { get { return DefineIdadeExtenso(); } set { } }
        [JsonIgnore]
        public ImageSource Foto { get; set; }

        public Crianca(string primeiro_nome, DateTime data_nascimento, int sexo)
        {
            crianca_primeiro_nome = primeiro_nome;
            crianca_dataNascimento = data_nascimento;
            crianca_sexo = sexo;
        }

        public Crianca() {}

        public string DefineIdadeExtenso()
        {
            if (IdadeSemanas < 4)
            {
                return SemanasToString();
            }
            else if (IdadeMeses < 12)
            {
                return MesesToString();// + " e " + SemanasToString();
            }
            else
            {   if (IdadeMeses > 12 && IdadeMeses < 13)
                {
                    return "1 ano";
                }
                else if (IdadeMeses > 24)
                {
                    return "2 anos";
                } else
                {
                    return "1 ano e " + MesesToString();
                }
            }
        }

        private string SemanasToString()
        {
            double semanas = IdadeSemanas;
            while (semanas > 4.34524)
            {
                semanas -= 4.34524;
            }

            int semanasAux = Convert.ToInt32(semanas);

            if (IdadeSemanas < 1)
            {
                return "recém-nascido";
            } else if (IdadeSemanas >= 1 && IdadeSemanas < 2)
            {
                return "1 semana";
            } else if (IdadeSemanas >= 2 && IdadeSemanas < 3)
            {
                return "2 semanas";
            } else
            {
                return "3 semanas";
            }
        }

        private string MesesToString()
        {
            int m = (IdadeMeses > 12) ? (int)Math.Floor(IdadeMeses - 12) : (int)Math.Floor(IdadeMeses);
            return m == 1 ? m + " mês" : m + " meses";
        }
    }
}