namespace Bomblix.SunnyPortal.Client
{
    public enum WindowsToOpen
    {
        Login,
        MessageBox,
    }

    internal class OpenWindowMessage
    {
        public WindowsToOpen WindowToOpen
        {
            get;set;
        }

        public bool IsModal
        {
            get;set;
        }
        public string Text { get; internal set; }
        public string Caption { get; internal set; }
    }
}