namespace ProMama.Model
{
    class Foto
    {
        public int Mes { get; private set; }
        public string Data { get; private set; }
        public string Imagem { get; private set; }

        public Foto(int mes, string data, string imagem)
        {
            Mes = mes;
            Data = data;
            Imagem = imagem;
        }
    }
}
