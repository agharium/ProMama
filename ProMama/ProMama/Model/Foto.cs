namespace ProMama.Model
{
    class Foto
    {
        public int Mes { get; set; }
        public string Data { get; set; }
        public string Imagem { get; set; }

        public Foto(int mes, string data, string imagem)
        {
            Mes = mes;
            Data = data;
            Imagem = imagem;
        }
    }
}
