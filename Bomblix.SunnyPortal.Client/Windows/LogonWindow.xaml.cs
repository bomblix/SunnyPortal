using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Bomblix.SunnyPortal.Client.Windows
{
    /// <summary>
    /// Interaction logic for LogonWindow.xaml
    /// </summary>
    public partial class LogonWindow : Window
    {
        public LogonWindow()
        {
            InitializeComponent();

            // FIX: Find better way...
            Messenger.Default.Register<Messages>( this, x =>
            {
                if ( x == Messages.IsLoggedIn )
                {
                    this.Close();
                }
            } );
        }
    }
}
