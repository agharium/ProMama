using Newtonsoft.Json;
using ProMama.Components;
using System;
using System.Collections.Generic;

namespace ProMama.Models
{
    public class Crianca
    {
        public int crianca_id { get; set; }
        public int user_id { get; set; }
        public string crianca_primeiro_nome { get; set; }
        public string crianca_sobrenome { get; set; }
        public DateTime crianca_dataNascimento { get; set; }
        public int crianca_sexo { get; set; }
        public double crianca_pesoAoNascer { get; set; }
        public double crianca_alturaAoNascer { get; set; }
        public string crianca_outrasInformacoes { get; set; }
        public int crianca_idade_gestacional { get; set; }
        public int crianca_tipo_parto { get; set; }
        public bool uploaded { get; set; }

        public List<int> notificacoesMarcadas { get; set; }

        // variáveis auxiliares
        [JsonIgnore]
        public double IdadeSemanas { get { return (DateTime.Now - crianca_dataNascimento).Days * 0.1551871428571429; } set { } }
        [JsonIgnore]
        public double IdadeMeses { get { return (DateTime.Now - crianca_dataNascimento).Days / 30.4167; } set { } }
        [JsonIgnore]
        public string IdadeExtenso { get { return Ferramentas.DaysToFullString((DateTime.Now - crianca_dataNascimento).Days, 1); } set { } }

        public Crianca(string primeiro_nome, DateTime data_nascimento, int sexo)
        {
            crianca_primeiro_nome = primeiro_nome;
            crianca_dataNascimento = data_nascimento;
            crianca_sexo = sexo;
            crianca_tipo_parto = -1;
            crianca_idade_gestacional = -1;
            uploaded = true;
        }

        public Crianca() {}
    }
}