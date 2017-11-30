using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Events;

namespace Scrolling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
        }

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var viewModel = (MainWindowModel)DataContext;

            var currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                viewModel.Formatter = x => new DateTime((long)x).ToString("t");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                viewModel.Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                viewModel.Formatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }

            viewModel.Formatter = x => new DateTime((long)x).ToString("yyyy");
        }

        private void Axis_OnPreviewRangeChanged(PreviewRangeChangedEventArgs e)
        {
            var viewModel = (MainWindowModel)DataContext;

            var percent = viewModel.MaxRange * 0.3;
            if (e.PreviewMinValue < viewModel.MinValueX - percent)
                e.Cancel = true;
            if (e.PreviewMaxValue > viewModel.MaxValueX + percent)
                e.Cancel = true;
        }
    }
}
