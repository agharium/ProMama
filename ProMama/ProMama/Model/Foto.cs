using Newtonsoft.Json;
using Xamarin.Forms;

namespace ProMama.Model
{
    public class Foto
    {
        public int id { get; set; }
        public int mes { get; set; }
        public string titulo { get; set; }
        public string caminho { get; set; }
        public int crianca { get; set; }
        public string url { get; set; }

        [JsonIgnore]
        public ImageSource source { get; set; }

        public Foto(int _mes, string _titulo, string _caminho, int _crianca)
        {
            mes = _mes;
            titulo = _titulo;
            caminho = _caminho == null ? "avatar_default.png" : _caminho;
            crianca = _crianca;

            source = caminho;
        }

        public Foto() { }
    }
}
