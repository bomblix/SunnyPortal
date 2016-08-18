using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Windows.Input;

namespace Bomblix.SunnyPortal.Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private Core.SunnyPortal sunnyPortal;
        private string userName;

        public LoginViewModel( Core.SunnyPortal sunnyPortal )
        {
            this.sunnyPortal = sunnyPortal;
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if ( value != userName )
                {
                    userName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Controls.PasswordBox>( Login );
            }
        }


        private void Login( System.Windows.Controls.PasswordBox password )
        {
            sunnyPortal.Connect( UserName, password.Password );

            if ( sunnyPortal.IsConnected )
            {
                Messenger.Default.Send<string>( "LoggedIn" );
            }
            else
            {
                Messenger.Default.Send<OpenWindowMessage>( new OpenWindowMessage { IsModal = true, WindowToOpen = WindowsToOpen.MessageBox, Caption = "Login", Text = "Invalid login or password" } );
            }
        }
    }
}
