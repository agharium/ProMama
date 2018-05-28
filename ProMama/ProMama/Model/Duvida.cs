using Newtonsoft.Json;

namespace ProMama.Model
{
    public class Duvida
    {
        public int duvida_id { get; set; }
        public int duvida_user { get; set; }
        public string duvida_pergunta { get; set; }
        public string duvida_resposta { get; set; }

        [JsonIgnore]
        public string duvida_resumo { get; set; }

        public Duvida(string pergunta, string resposta)
        {
            duvida_pergunta = pergunta;
            duvida_resposta = resposta;
            duvida_resumo = resposta;
        }

        public Duvida() { }
    }
}
