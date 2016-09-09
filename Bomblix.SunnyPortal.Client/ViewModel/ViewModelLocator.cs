using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Bomblix.SunnyPortal.Client.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider( () => SimpleIoc.Default );

            SimpleIoc.Default.Register<Core.SunnyPortal>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LiveDataModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<DailyDataViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LiveDataModel LiveData
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LiveDataModel>();
            }
        }
        
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public DailyDataViewModel DailyData
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DailyDataViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}