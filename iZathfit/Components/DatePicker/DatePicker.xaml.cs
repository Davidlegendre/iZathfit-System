using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace iZathfit.Components.DatePicker
{
    /// <summary>
    /// Lógica de interacción para DatePicker.xaml
    /// </summary>
    public partial class DatePicker : UserControl, INotifyPropertyChanged
    {
        public DatePicker()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += DatePicker_Loaded;
            
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        string _MonthSeleted = "";
        string _DaySelected = "";
       
        string _YearSelected = "";

        DateTime _DateSelected = DateTime.Now;
        public DateTime DateSelect
        {
            get => _DateSelected;
            set { 
                if(value != _DateSelected)
                {
                    _DateSelected = value;
                    DaySelected = DateSelect.Day.ToString("00");
                    MonthSeleted = DateSelect.Date.ToString("MMMM");
                    YearSelected = DateSelect.Year.ToString("0000");
                    if(DateSelectChanged != null)
                    {
                        DateSelectChanged.Invoke(this, DateSelect);
                    }
                    change(nameof(DateSelect));
                    change(nameof(MonthSeleted));
                    change(nameof(DaySelected));
                    change(nameof(YearSelected));
                }
            }
        }

        public event EventHandler<DateTime>? DateSelectChanged;


        public string MonthSeleted
        {
            get => _MonthSeleted;
            set
            {
                if (value != _MonthSeleted)
                {
                    _MonthSeleted = value;
                   

                }
            }
        }

        public string DaySelected
        {
            get => _DaySelected;
            set { 
                if(value  != _DaySelected)
                {
                    _DaySelected = value;
                    
                }
            }
        }

        public string YearSelected {
            get => _YearSelected;
            set
            {
                if(value != _YearSelected)
                {
                    _YearSelected = value;
                    
                }
            }
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



        Visibility _HeaderVisible = Visibility.Collapsed;
        public Visibility HeaderVisible
        {
            get => _HeaderVisible;
            set { _HeaderVisible = value; change(nameof(HeaderVisible)); }
        }

        private void Clock_click(object sender, RoutedEventArgs e)
        {
            var cardaction = (Wpf.Ui.Controls.CardAction)sender;
            var grid = cardaction.Content as Grid;
            var flyout = grid?.Children[grid.Children.Count - 1] as Flyout;
            if (flyout != null)
            {
                flyout.Show();
                //listHour.ScrollIntoView(listHour.SelectedItem);
                //listMinute.ScrollIntoView(listMinute.SelectedItem);
                //listSecond.ScrollIntoView(listSecond.SelectedItem);
            }

        }

        void change(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
