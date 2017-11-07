using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel
{
    class IntroducaoModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }

        public IntroducaoModel()
        {
            this.NavigationCommand = new Command(this.Navigation);
        }

        private void Navigation()
        {

        }
    }
}
