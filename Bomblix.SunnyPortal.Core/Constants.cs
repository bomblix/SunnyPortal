using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomblix.SunnyPortal.Core
{
    internal class Constants
    {
        internal const string UserNameParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$txtUserName";
        internal const string PasswordParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$txtPassword";
        internal const string LoginButtonParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$LoginBtn";
        internal const string ServiceAccessParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$ServiceAccess";
        internal const string EventTargetParamerter = "__EVENTTARGET";
        internal const string EventArgumentParameter = "__EVENTARGUMENT";
        internal const string ViewStateParameter = "__VIEWSTATE";
        internal const string ViewStateGeneratorParameter = "__VIEWSTATEGENERATOR";
        internal const string HiddenLanguageParameter = "ctl00$ContentPlaceHolder1$hiddenLanguage";
        internal const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.82 Safari/537.36 OPR/39.0.2256.43";
        internal const string RequestMethod = "POST";

        internal const string DateSelectionDatePicker = "ctl00$ContentPlaceHolder1$UserControlShowInverterSelection1$_datePicker";

        internal const string PortalUrl = "https://www.sunnyportal.com/";
        internal const string LoginUrl = PortalUrl + "Templates/Start.aspx?logout=true";
        internal const string DownloadUrl = PortalUrl + "Templates/DownloadDiagram.aspx?down=diag";
        internal const string SelectDateUrl = PortalUrl + "FixedPages/InverterSelection.aspx";
        internal const string LiveDataUrl = PortalUrl + "homemanager?t={0}";

        internal const string UserNameLabelName = "ctl00_Header_lblUserName";
        internal const string PortalCulture = "en-us";
        internal const string DateSelectionIntervalId = "ctl00$ContentPlaceHolder1$UserControlShowInverterSelection1$SelectedIntervalID";
        internal const string DateSelectionDateTextBox = "ctl00$ContentPlaceHolder1$UserControlShowInverterSelection1$_datePicker$textBox";
        internal const string DateFormat = "d/M/yyyy";

        internal const string LoginButtonValue = "Login";
    }
}
