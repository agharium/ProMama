namespace ProMama.Model
{
    class Foto
    {
        public int Mes;
        public string Data;
        public string Imagem;

        public Foto(int mes, string data, string imagem)
        {
            Mes = mes;
            Data = data;
            Imagem = imagem;
        }
    }
}
