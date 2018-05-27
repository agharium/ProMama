using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace ProMama.Model
{
    public class Marco
    {
        public int id { get; set; }
        public int marco { get; set; }
        public DateTime data { get; set; }
        public string extra { get; set; }

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
        [JsonIgnore]
        public Color TextoBackgroundColor { get; set; }

        public Marco(string _titulo, Color _tituloBackgroundColor, bool _alcancado, ImageSource _imagem)
        {
            Titulo = _titulo;
            Alcancado = _alcancado;
            Imagem = _imagem;
            TituloBackgroundColor = _tituloBackgroundColor;

            Texto = Alcancado ? "Já alcançado" : "Não alcançado";
            TextoBackgroundColor = Alcancado ? Color.ForestGreen : Color.Orange;
        }

        public Marco() { }
    }
}
