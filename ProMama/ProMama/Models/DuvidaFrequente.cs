using System.Collections.Generic;

namespace ProMama.Models
{
    public class DuvidaFrequente
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string texto { get; set; }
        public string resumo { get; set; }
        public List<Link> links { get; set; }

        public DuvidaFrequente() { }
    }
}
