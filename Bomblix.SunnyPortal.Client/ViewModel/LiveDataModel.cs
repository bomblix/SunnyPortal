﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Timers;

namespace Bomblix.SunnyPortal.Client.ViewModel
{
    public class LiveDataModel : ViewModelBase
    {
        private int currentPower;
        private Core.SunnyPortal sunnyPortal;
        private Timer timer;

        public int CurrentPower
        {
            get
            {
                return currentPower;
            }
            set
            {
                if ( currentPower != value )
                {
                    currentPower = value;
                    RaisePropertyChanged();
                }
            }
        }

        public LiveDataModel( Core.SunnyPortal service )
        {
            sunnyPortal = service;

            timer = new Timer( 10000 );
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;

            Messenger.Default.Register<Messages>( this, async ( x ) =>
            {
                if ( x == Messages.IsLoggedIn )
                {
                    this.CurrentPower = await sunnyPortal.GetCurrentPower();
                    this.timer.Start();
                }
            } );
        }

        private async void Timer_Elapsed( object sender, ElapsedEventArgs e )
        {
            this.CurrentPower = await sunnyPortal.GetCurrentPower();
        }
    }
}
