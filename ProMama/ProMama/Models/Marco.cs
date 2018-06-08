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

        [JsonIgnore]
        public string Titulo { get; set; }
        [JsonIgnore]
        public string Texto { get; set; }
        [JsonIgnore]
        private bool _alcancado;
        public bool Alcancado
        {
            get
            {
                return _alcancado;
            }
            set
            {
                _alcancado = value;
                Texto = value ? "Já alcançado" : "Não alcançado";
                TextoBackgroundColor = value ? Color.FromHex("#4CAF50") : Color.FromHex("#FF9800");
            }
        }
        [JsonIgnore]
        public ImageSource Imagem { get; set; }

        [JsonIgnore]
        public Color TituloBackgroundColor { get; set; }
        [JsonIgnore]
        public Color TextoBackgroundColor { get; set; }
        [JsonIgnore]
        public bool Visivel { get; set; }

        public Marco(int _marco, string _titulo, Color _tituloBackgroundColor, bool _alcancado, ImageSource _imagem)
        {
            marco = _marco;

            Titulo = _titulo;
            Alcancado = _alcancado;
            Imagem = _imagem;
            TituloBackgroundColor = _tituloBackgroundColor;
            
            Visivel = false;
        }

        public Marco() { }
    }
}
