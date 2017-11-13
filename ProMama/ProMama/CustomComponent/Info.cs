using System;

namespace ProMama.CustomComponent
{
    public class Info
    {
        public int idadeInicio { get; set; }
        public int idadeFim { get; set; }
        public String titulo { get; set; }
        public String texto { get; set; }
        public String imagem { get; set; }

        public Info(int idadeInicio, int idadeFim, String titulo, String texto, String imagem)
        {
            this.idadeInicio = idadeInicio;
            this.idadeFim = idadeFim;
            this.titulo = titulo;
            this.texto = texto;
            this.imagem = imagem;
        }

    }
}
