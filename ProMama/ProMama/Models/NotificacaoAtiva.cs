namespace ProMama.Models
{
    public class NotificacaoAtiva
    {
        public int id { get; set; }
        public int crianca_id { get; set; }
        public int notificacao_id { get; set; }

        public NotificacaoAtiva(int crianca_id, int notificacao_id)
        {
            this.crianca_id = crianca_id;
            this.notificacao_id = notificacao_id;
        }
    }
}
