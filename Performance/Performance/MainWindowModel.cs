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
            set 
            { 
                plotModel = value; 
                OnPropertyChanged("PlotModel"); 
            }
        }

        private DateTime lastUpdate = DateTime.Now;

        LineSeries[] lineSerie = new LineSeries[3];

        public MainWindowModel()
        {
            PlotModel = new PlotModel();
            SetUpModel();
            LoadData();
            if (lineSerie[0] == null)
                InitLineSeries();

        }

        private void InitLineSeries()
        {
            for (int j = 0; j < 3; j++)
            {
                lineSerie[j] = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = OxyColors.Red,  //colors[data.Key],
                    MarkerType = MarkerType.Plus,
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Detector {0}", 0),
                    Smooth = false,
                };
            }
        }

        private readonly List<OxyColor> colors = new List<OxyColor>
        {
            OxyColors.Green,
            OxyColors.IndianRed,
            OxyColors.Coral,
            OxyColors.Chartreuse,
            OxyColors.Azure
        };


        Measurement[] measure = new Measurement[100];

        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Vertical;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Date", "HH:mm");
            dateAxis.MajorGridlineStyle = LineStyle.Solid;
            dateAxis.MinorGridlineStyle = LineStyle.Dot;
            dateAxis.IntervalLength = 80;
            PlotModel.Axes.Add(dateAxis);

            var valueAxis = new LinearAxis(AxisPosition.Left, 0) 
            { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModel.Axes.Add(valueAxis);

        }



        private void LoadData(/*Queue<Measurement> qMeasurement*/)
        {
            //List<Measurement> measurements = new List<Measurement>();

            //var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            LoadModel();
            //UpdateModel();
        }

        public void LoadModel()
        {
            for (int j = 0; j < 3; j++)
            {

                if (lineSerie[0] == null)
                    InitLineSeries();

                for (int i = 0; i < 5; i++)
                {
                    measure[i] = new Measurement();
                    //measure[i].DateTime = new DateTime(2008, 5, 1, 8, 30, 5 + i);
                    measure[i].DateTime = DateTime.Now;
                    measure[i].DetectorId = j;
                    measure[i].Value = i * 2 + j;
                    DataPoint dataPoint = new DataPoint(DateTimeAxis.ToDouble(measure[i].DateTime), measure[i].Value);
                    lineSerie[j].Points.Add(dataPoint);
                }

                PlotModel.Series.Add(lineSerie[j]);
            }

            lastUpdate = DateTime.Now;

            //OnPropertyChanged("PlotModel");
        }

        public void UpdateModel()
        {
            for (int j = 0; j < 3; j++)
            {

                for (int i = 0; i < 20; i++)
                {
                    measure[i] = new Measurement();
                    measure[i].DateTime = new DateTime(2008, 5, 1, 8, 30, 15 + i);
                    measure[i].DetectorId = j;
                    measure[i].Value = i * 30 + j;
                    DataPoint dataPoint = new DataPoint(DateTimeAxis.ToDouble(measure[i].DateTime), measure[i].Value);
                    lineSerie[j].Points.Add(dataPoint);
                }

                //PlotModel.Series.Add(lineSerie[j]);
            }
            lastUpdate = DateTime.Now;

            OnPropertyChanged("PlotModel");

        }

        private List<Measurement> GetData(int date)
        {

            List < Measurement > retVal = new List<Measurement> ();
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    Measurement measure = new Measurement();
                    measure.DateTime = new DateTime(2008, 5, 1, 8, 30, 15 + i);
                    measure.DetectorId = j;
                    measure.Value = i * 30 + j;
                    retVal.Add(measure);
                }
            }
            return retVal;
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
