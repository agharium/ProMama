namespace ProMama.Model
{
    class Crianca
    {
        public string PrimeiroNome { get; set; }
        public string SegundoNome { get; set; }
        public int IdadeSemanas { get; set; }
        public int IdadeMeses { get; set; }
        public string IdadeExtenso { get; set; }

        public Crianca(string primeiroNome, int idadeSemanas)
        {
            PrimeiroNome = primeiroNome;
            IdadeSemanas = idadeSemanas;

            IdadeMeses = idadeSemanas / 4;
            defineIdadeExtenso();
        }

        private void defineIdadeExtenso()
        {
            if (IdadeMeses == 0)
            {
                if (IdadeSemanas == 1)
                {
                    IdadeExtenso = "1 semana";
                }
                else
                {
                    IdadeExtenso = IdadeSemanas + " semanas";
                }
            }
            else if (IdadeMeses >= 12)
            {
                if (IdadeMeses == 12)
                {
                    IdadeExtenso = "1 ano";
                }
                else if (IdadeMeses >= 24)
                {
                    IdadeExtenso = "2 anos";
                }
                else
                {
                    if (IdadeMeses - 12 == 1)
                    {
                        IdadeExtenso = "1 ano e 1 mês";
                    } else
                    {
                        IdadeExtenso = "1 ano e " + (IdadeMeses - 12) + " meses";
                    }
                }
            }
            else
            {
                if (IdadeMeses == 1)
                {
                    IdadeExtenso = "1 mês";
                } else
                {
                    IdadeExtenso = IdadeMeses + " meses";
                }
                
            }
        }
    }
}
