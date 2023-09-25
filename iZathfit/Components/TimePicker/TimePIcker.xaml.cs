using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace iZathfit.Components.TimePicker
{
    /// <summary>
    /// Lógica de interacción para TimePIcker.xaml
    /// </summary>
    public partial class TimePIcker : UserControl, INotifyPropertyChanged, IDisposable
    {
        HelperTimerPicker? _timerPicker;
        public TimePIcker()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += TimePIcker_Loaded;
        }

        private void TimePIcker_Loaded(object sender, RoutedEventArgs e)
        {
            _timerPicker = new HelperTimerPicker();
            Hours = _timerPicker.Hours();
            Minutes = _timerPicker.Minutes();
            Seconds = _timerPicker.Seconds();
            AMPM = _timerPicker.AMPM();
        }
        public DateTime TimeSelected { get => DateTime.Parse(HourSelected + ":" + MinuteSelected + ":" + SecondSelected + " " + AMPPMSelected);
            set {
                HourSelected = value.ToString("hh");
                MinuteSelected = value.ToString("mm");
                SecondSelected = value.ToString("ss");
                AMPPMSelected = value.Hour > 12 ? "p. m." : "a. m.";
            }
        }

        string _Hourselected = DateTime.Now.ToString("hh");
        string _minuteselected = DateTime.Now.ToString("mm");
        string _secondselected = DateTime.Now.ToString("ss");
        string _ampmselected = DateTime.Now.Hour > 12 ? "p. m." : "a. m.";

        public string AMPPMSelected
        {
            get => _ampmselected;
            set
            {
                if (value != _ampmselected)
                {
                    _ampmselected = value;
                    change(nameof(AMPPMSelected));
                }
            }
        }


        public string HourSelected {
            get => _Hourselected;
            set {
                if (value != _Hourselected)
                { 
                    _Hourselected= value;
                    change(nameof(HourSelected));
                }
            }
        }

        public string MinuteSelected
        {
            get => _minuteselected;
            set
            {
                if (value != _minuteselected)
                {
                    _minuteselected = value;
                    change(nameof(MinuteSelected));
                }
            }
        }

        public string SecondSelected
        {
            get => _secondselected;
            set
            {
                if (value != _secondselected)
                {
                    _secondselected = value;
                    change(nameof(SecondSelected));
                }
            }
        }


        string _titulo = "";
        public string Titulo {
            get => _titulo;
            set {
                if (value != _titulo)
                {
                    _titulo = value;
                    change(nameof(Titulo));
                }
            }
        }

        ObservableCollection<string> _hours = new ObservableCollection<string>();
        public ObservableCollection<string> Hours {
            get => _hours;
            set
            {
                if(value != _hours)
                {
                    _hours = value;
                    change(nameof(Hours));
                }
            }
        }

        ObservableCollection<string> _minutes = new ObservableCollection<string>();
        public ObservableCollection<string> Minutes
        {
            get => _minutes;
            set
            {
                if (value != _minutes)
                {
                    _minutes = value;
                    change(nameof(Minutes));
                }
            }
        }

        ObservableCollection<string> _seconds = new ObservableCollection<string>();
        public ObservableCollection<string> Seconds
        {
            get => _seconds;
            set
            {
                if (value != _seconds)
                {
                    _seconds = value;
                    change(nameof(Seconds));
                }
            }
        }

        ObservableCollection<string> _ampm = new ObservableCollection<string>();
        public ObservableCollection<string> AMPM { 
            get => _ampm;
            set
            {
                if (value != _ampm)
                {
                    _ampm = value;
                    change(nameof(AMPM));
                }
            }
        }

        Visibility _HeaderVisible = Visibility.Collapsed;
        public Visibility HeaderVisible { get => _HeaderVisible;
            set {_HeaderVisible = value; change(nameof(HeaderVisible)); }
        }

        string _header = "Tiempo";
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                change(nameof(Header));
            }
        }

        //string _hour = DateTime.Now.Hour.ToString("hh");
        //string _minutes = DateTime.Now.Minute.ToString();
        //string _seconds = DateTime.Now.Second.ToString();
        //string _ampm = DateTime.Now.TimeOfDay.ToString();

        bool _isSecondsVisible = true;
        public bool IsSecondsVisible { 
            get => _isSecondsVisible;
            set {
                if (value != _isSecondsVisible)
                {
                    _isSecondsVisible = value;
                    SecondsVisible = IsSecondsVisible ? Visibility.Visible : Visibility.Collapsed;
                    ColumnsGrid = IsSecondsVisible ? ColumnsGrid = 4 : ColumnsGrid = 3;
                    SecondSelected = "00";
                    change(nameof(SecondSelected));
                    change(nameof(SecondsVisible));
                    change(nameof(ColumnsGrid));
                }
            }
        }

        Visibility _secondsVisible = Visibility.Visible;
        public Visibility SecondsVisible {
            get => _secondsVisible;
            set { 
                if(value != _secondsVisible)
                {
                    _secondsVisible = value;
                }
            }
        }

        int _columnsGrid = 4;
        public int ColumnsGrid
        {
            get => _columnsGrid;
            set
            {
                if (value != _columnsGrid)
                {
                    _columnsGrid = value;
                }
            }
        }

        void change(string propiedad)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Clock_click(object sender, RoutedEventArgs e)
        {
            var cardaction = (Wpf.Ui.Controls.CardAction)sender;
            var grid = cardaction.Content as Grid;
            var flyout = grid?.Children[grid.Children.Count -1] as Flyout;
            if (flyout != null)
            {                               
                flyout.Show();
                listHour.ScrollIntoView(listHour.SelectedItem);
                listMinute.ScrollIntoView(listMinute.SelectedItem);
                listSecond.ScrollIntoView(listSecond.SelectedItem);
            }
            
        }

        public void Dispose()
        {
            _timerPicker = null;
            Minutes.Clear();
            Seconds.Clear();
            AMPM.Clear();
        }
    }
}
