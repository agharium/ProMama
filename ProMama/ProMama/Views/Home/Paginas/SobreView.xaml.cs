using ProMama.Components;
using ProMama.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SobreView : ContentPage
    {
        private Aplicativo app = Aplicativo.Instance;
        public SobreView()
        {
            InitializeComponent();
            
            Texto.Children.Add(
                Ferramentas.CreateBrowser(
                    "Com o objetivo de aumentar os índices de aleitamento materno, qualificar a assistência no município de Osório e nortear a conduta dos profissionais da Atenção Básica, a equipe de fonoaudiologia e o NASF criaram com o apoio do Secretário Emerson Arli Magni da Silva e o Prefeito Eduardo Aluísio Cardoso Abrahão o Programa Municipal de Aleitamento Materno intitulado PRÓ-MAMÁ."
                    + "<br><br>" +
                    "Este programa está inserido na Linha de Cuidado Materno-Infantil, pertencente a Rede Cegonha em Osório e foi oficializado pelo Decreto 193/2017 que dispõe sobre o Programa Municipal de Aleitamento Materno - Pró-Mamá, através do Protocolo Municipal de Aleitamento Materno de Osório."
                    + "<br><br>" +
                    "Neste programa, destaca-se a formação de profissionais de referência em amamentação em cada Unidade de Saúde da Família, que tem por objetivo de atuar de forma mais próxima ao território e aos usuários nas dificuldades em relação à amamentação. Tais profissionais são capacitados constantemente pela Equipe Gestora Multidisciplinar do Pró-Mamá, composta por fonoaudiólogo, nutricionista e psicólogo. Estas referências atuam diretamente no acompanhamento da dupla mãe-bebê em relação ao aleitamento materno e desenvolvimento infantil através de visita domiciliar e atendimentos na Unidade de Saúde no intuito de evitar a introdução de fórmulas infantis ou leite de vaca. Os casos mais complexos são discutidos e encaminhados para a equipe multiprofissional do Programa. Outras ações desenvolvidas pelo PRÓ-MAMÁ compreendem grupos, orientação, sala de espera, visitas domiciliares, interconsultas e discussão de casos."
                    + "<br><br>" +
                    "O PRÓ-MAMÁ atua de forma intersetorial em parceria com o Programa Jogue Limpo, a Secretaria de Assistência Social e Habitação, Secretaria de Educação, UniCNEC, IFRS Campus Osório entre outros."
                    + "<br><br>" +
                    "Devido ao leite materno ser considerado o melhor e mais completo alimento para o bebê, o 'padrão ouro' da alimentação infantil e com o objetivo de fornecer informação de qualidade para todos que assim desejarem, independente de procurar o Sistema Único de Saúde - SUS, a equipe gestora do PRÓ-MAMÁ  juntamente com a comunidade acadêmica do Instituto Federal do Rio Grande do Sul – IFRS - Campus Osório desenvolveu este aplicativo municipal voltado à amamentação e desenvolvimento infantil. O aplicativo foi legitimado através do Acordo de Cooperação, que entre si celebram o Instituto Federal de Educação, Ciência e Tecnologia do Rio Grande do Sul - IFRS Campus Osório e a Prefeitura Municipal de Osório para que o desenvolvimento do aplicativo fosse possível. A partir de então, em abril de 2017 iniciaram-se as reuniões nas quais a Equipe Multiprofissional do PRÓ-MAMÁ, os professores e alunos do Instituto Federal do Rio Grande do Sul – IFRS trabalharam arduamente no desenvolvimento deste aplicativo. Neste período contamos também com apoio técnico-administrativo do Alan Alves Correa, a assessoria do DTI – Departamento de Tecnologia da Informação, a parceria e colaboração da Coordenação da Atenção Básica, do Secretário da Saúde Emerson Arli Magni da Silva. Após aproximadamente 18 meses desde o surgimento da ideia até o lançamento deste aplicativo concretiza-se um sonho, integrar a comunidade acadêmica e os serviços de saúde em Osório culminando com um aplicativo de excelente qualidade voltado para a comunidade osoriense, mas acessível a todos que assim desejarem através das plataformas IOS e Android.")
                );
        }

        protected override bool OnBackButtonPressed()
        {
            Aplicativo app = Aplicativo.Instance;
            app._home.Detail_Home();
            return true;
        }

        void OnDecretoTapped(object sender, EventArgs args)
        {
            try
            {
                Device.OpenUri(new Uri("https://leismunicipais.com.br/a/rs/o/osorio/decreto/2017/20/193/decreto-n-193-2017-dispoe-sobre-o-programa-municipal-de-aleitamento-materno-pro-mama-atraves-do-protocolo-municipal-de-aleitamento-materno-de-osorio?q=193%2F2017"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        void OnDesenvolvedoresTapped(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new DesenvolvedoresView());
        }
    }
}
