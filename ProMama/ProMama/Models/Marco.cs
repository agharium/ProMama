using Newtonsoft.Json;
using Xamarin.Forms;

namespace ProMama.Models
{
    public class Marco
    {
        public int id { get; set; }
        public int crianca { get; set; }
        public int marco { get; set; }
        public string data { get; set; }
        public string extra { get; set; }
        public bool uploaded { get; set; }

        [JsonIgnore]
        public string Titulo { get; set; }
        [JsonIgnore]
        public string Texto { get; set; }
        [JsonIgnore]
        public bool Alcancado { get; set; }
        [JsonIgnore]
        public ImageSource Imagem { get; set; }
        [JsonIgnore]
        public Color TituloBackgroundColor { get; set; }

        public Marco(int _marco, string _titulo, Color _tituloBackgroundColor, bool _alcancado, ImageSource _imagem)
        {
            marco = _marco;

            Titulo = _titulo;
            Alcancado = _alcancado;
            Imagem = _imagem;
            TituloBackgroundColor = _tituloBackgroundColor;
            uploaded = false;
        }

        public Marco() { }
    }
}
