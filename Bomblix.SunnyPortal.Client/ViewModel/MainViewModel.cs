using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Bomblix.SunnyPortal.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string connectionStatus;
        private bool isConnected;

        private Core.SunnyPortal sunnyPortal;

        public MainViewModel(Core.SunnyPortal sunnyPortal)
        {
            this.sunnyPortal = sunnyPortal;
            connectionStatus = "Disconneted";

            Messenger.Default.Register<string>( this, ( x ) =>
            {
                if ( x == "LoggedIn" )
                {
                    HandleMessage();
                }
            } );


        }

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                if ( value != isConnected )
                {
                    isConnected = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ConnectionStatus
        {
            get
            {
                return connectionStatus;
            }
            set
            {
                if ( value != connectionStatus )
                {
                    connectionStatus = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand LogInCommand
        {
            get
            {
                return new RelayCommand( LogIn );
            }
        }

        private void LogIn()
        {
            Messenger.Default.Send<OpenWindowMessage>( new OpenWindowMessage { IsModal = true, WindowToOpen = WindowsToOpen.Login } );
        }

        private void HandleMessage()
        {
            this.IsConnected = sunnyPortal.IsConnected;
        }
    }
}