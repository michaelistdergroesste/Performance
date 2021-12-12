using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Performance
{
    internal class MainWindowModel : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        private DateTime lastUpdate = DateTime.Now;

        public MainWindowModel()
        {
            PlotModel = new PlotModel();
            SetUpModel();
            LoadData();
        }

        private readonly List<OxyColor> colors = new List<OxyColor>
        {
            OxyColors.Green,
            OxyColors.IndianRed,
            OxyColors.Coral,
            OxyColors.Chartreuse,
            OxyColors.Azure
        };

        private readonly List<MarkerType> markerTypes = new List<MarkerType>
        {
            MarkerType.Plus,
            MarkerType.Star,
            MarkerType.Diamond,
            MarkerType.Triangle,
            MarkerType.Cross
        };


        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Vertical;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Date", "HH:mm") { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis(AxisPosition.Left, 0) { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModel.Axes.Add(valueAxis);

        }



        private void LoadData(/*Queue<Measurement> qMeasurement*/)
        {
            List<Measurement> measurements = new List<Measurement>();
            
            var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            //foreach (var data in dataPerDetector)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = OxyColors.Green,  //colors[data.Key],
                    MarkerType = MarkerType.Plus,
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Detector {0}", 0),
                    Smooth = false,
                };
                Measurement[] measure = new Measurement[5];
                for (int i = 0; i < 5; i++)
                {
                    measure[i] = new Measurement();
                    measure[i].DateTime = new DateTime(2008, 5, 1, 8, 30, 5 + i);
                    measure[i].DetectorId = 0;
                    measure[i].Value = i * 2;
                    DataPoint dataPoint = new DataPoint(DateTimeAxis.ToDouble(measure[i].DateTime), measure[i].Value);
                    lineSerie.Points.Add(dataPoint);


                }
                
                PlotModel.Series.Add(lineSerie);
            }
            lastUpdate = DateTime.Now;
        }

        public void UpdateModel()
        {
            //List<Measurement> measurements = Data.GetUpdateData(lastUpdate);
            //var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            //foreach (var data in dataPerDetector)
            //{
            //    var lineSerie = PlotModel.Series[data.Key] as LineSeries;
            //    if (lineSerie != null)
            //    {
            //        data.ToList()
            //            .ForEach(d => lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(d.DateTime), d.Value)));
            //    }
            //}
            //lastUpdate = DateTime.Now;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
