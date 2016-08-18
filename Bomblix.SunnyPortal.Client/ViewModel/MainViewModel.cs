using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Bomblix.SunnyPortal.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Core.SunnyPortal sunnyPortal;
        private bool canUserConnecting;

        public MainViewModel( Core.SunnyPortal sunnyPortal )
        {
            this.sunnyPortal = sunnyPortal;
            this.CanUserConnecting = true;

            Messenger.Default.Register<Messages>( this, ( x ) =>
            {
                if ( x == Messages.IsLoggedIn )
                {
                    HandleMessage();
                }
            } );
        }

        public bool CanUserConnecting
        {
            get
            {
                return canUserConnecting;
            }
            set
            {
                if ( value != canUserConnecting )
                {
                    canUserConnecting = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged( "ConnectionStatus" );
                }
            }
        }

        public string ConnectionStatus
        {
            get
            {
                return CanUserConnecting ? "Disconneted" : "Connected";
            }
        }

        public ICommand LogInCommand
        {
            get
            {
                return new RelayCommand( OnLogin );
            }
        }

        private void OnLogin()
        {
            Messenger.Default.Send<OpenWindowMessage>( 
                new OpenWindowMessage
                {
                    IsModal = true,
                    WindowToOpen = WindowsToOpen.Login
                } );
        }

        private void HandleMessage()
        {
            this.CanUserConnecting = !sunnyPortal.IsConnected;
        }
    }
}