using Newtonsoft.Json;

namespace ProMama.Models
{
    public class DuvidaFrequente
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string texto { get; set; }

        [JsonIgnore]
        public string resumo { get; set; }

        public DuvidaFrequente() { }
    }
}
