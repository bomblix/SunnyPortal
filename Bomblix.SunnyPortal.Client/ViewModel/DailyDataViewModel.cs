using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OxyPlot;
using System;
using System.Collections.Generic;

namespace Bomblix.SunnyPortal.Client.ViewModel
{
    public class DailyDataViewModel : ViewModelBase
    {
        private Core.SunnyPortal sunnyPortal;
        private PlotModel plotModel;

        public DailyDataViewModel( Core.SunnyPortal service )
        {
            sunnyPortal = service;
            Messenger.Default.Register<Messages>( this, ( x ) =>
            {
                if ( x == Messages.IsLoggedIn )
                {
                    LoadPoints();
                }
            } );
        }

        private async void LoadPoints()
        {
            var todaysData = await sunnyPortal.GetHistoricalData( DateTime.Today );
            var points = new List<DataPoint>();

            var i = 0;

            foreach ( var item in todaysData )
            {
                points.Add( new DataPoint( i++, item.Value ) );
            }

            PreparePlot( points );
        }

        private void PreparePlot( List<DataPoint> temp )
        {
            var series = new LineSeries( OxyColors.Red );
            series.Points = temp;
            this.PlotModel = new PlotModel( "Today" );
            var xAxis = new LinearAxis( AxisPosition.Bottom );
            PlotModel.Axes.Add( xAxis );
            var yAxis = new LinearAxis( AxisPosition.Left, 0.0, 5.0 );
            PlotModel.Axes.Add( yAxis );
            PlotModel.Series.Add( series );
        }

        public PlotModel PlotModel
        {
            get
            {
                return plotModel;
            }
            set
            {
                if ( value != plotModel )
                {
                    plotModel = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
