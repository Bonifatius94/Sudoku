using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sudoku.UI.Main
{
    /// <summary>
    /// Interaction logic for MainViewModel.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            // get initial window size from xaml
            initialHeight = this.Height;
            initialWidth = this.Width;
        }

        #region ScaleValue_DepdencyProperty

        private double initialHeight;
        private double initialWidth;

        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainView), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainView mainWindow = o as MainView;

            if (mainWindow != null)
            {
                return mainWindow.OnCoerceScaleValue((double)value);
            }
            else
            {
                return value;
            }
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainView mainWindow = o as MainView;

            if (mainWindow != null)
            {
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
            {
                return 1.0f;
            }

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {
        }

        public double ScaleValue
        {
            get { return (double)GetValue(ScaleValueProperty); }
            set { SetValue(ScaleValueProperty, value); }
        }

        #endregion ScaleValue_DepdencyProperty

        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            CalculateScale();
        }

        private void CalculateScale()
        {
            // relate actual dimensions to initial dimensions
            double yScale = ActualHeight / initialHeight;
            double xScale = ActualWidth / initialWidth;
            double value = Math.Min(xScale, yScale);
            ScaleValue = (double)OnCoerceScaleValue(myMainWindow, value);
        }
    }
}
