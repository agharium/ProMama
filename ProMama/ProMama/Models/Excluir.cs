using System.Collections.Generic;

namespace ProMama.Models
{
    public class Excluir
    {
        public int Id { get; set; }
        public List<int> Criancas { get; set; }
        public List<int> Fotos { get; set; }

        public Excluir(int id)
        {
            Id = id;
            Criancas = new List<int>();
            Fotos = new List<int>();
        }

        public Excluir() { }
    }
}
