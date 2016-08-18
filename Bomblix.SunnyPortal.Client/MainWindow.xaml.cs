using Bomblix.SunnyPortal.Client.Windows;
using System.Windows;

namespace Bomblix.SunnyPortal.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // To improve... Temporary version
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<OpenWindowMessage>( this, ( message ) =>
            {
                Window window = null;

                if ( message.WindowToOpen == WindowsToOpen.Login )
                {
                    window = new LogonWindow();
                }

                else if (message.WindowToOpen == WindowsToOpen.MessageBox )
                {
                    MessageBox.Show( message.Text, message.Caption );
                    return;
                }

                if ( window == null )
                {
                    return;
                }

                if ( message.IsModal )
                {
                    window.ShowDialog();
                    return;
                }

                window.Show();

            } );
        }
    }
}
