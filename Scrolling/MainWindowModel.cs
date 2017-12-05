using System;
using System.ComponentModel;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;

namespace Scrolling
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public object Mapper { get; set; }
        public ChartValues<DateTimePoint> Values { get; set; }

        public double MinValueX => Values.Min(x => x.DateTime).Ticks;
        public double MaxValueX => Values.Max(x => x.DateTime).Ticks;
        public double MaxRange => (MaxValueX - MinValueX) * 1.1;


        public MainWindowModel()
        {
            Values = new ChartValues<DateTimePoint>();

            var trend = 50d;
            var random = new Random();
            var timeStep = DateTime.Now;
            for (var i = 0; i < 500; i++)
            {
                timeStep = timeStep.AddHours(1);
                Values.Add(new DateTimePoint(timeStep.AddDays(i*10), trend));

                if (random.NextDouble() > 0.4)
                {
                    trend += random.NextDouble()*10;
                }
                else
                {
                    trend -= random.NextDouble()*10;
                }
            }

            Formatter = x => new DateTime((long) x).ToString("yyyy");
            From = Values.Min(x => x.DateTime).Ticks;
            To = DateTime.Now.AddHours(90000).Ticks;
        }

        #region ViewModelProperties
        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged(nameof(Formatter));
            }
        }

        private double _from;
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        private double _to;
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}