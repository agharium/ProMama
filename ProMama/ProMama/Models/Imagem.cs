using Newtonsoft.Json;
using Xamarin.Forms;

namespace ProMama.Models
{
    public class Imagem
    {
        public int id { get; set; }
        public string caminho { get; set; }
        public int informacao { get; set; }

        [JsonIgnore]
        public ImageSource source { get; set; }

        public Imagem() { }
    }
}
