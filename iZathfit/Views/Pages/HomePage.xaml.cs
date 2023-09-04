using iZathfit.ViewModels.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePageVM vm { get; }
        public HomePage()
        {
            vm = App.GetService<HomePageVM>();
            DataContext = vm;
            InitializeComponent();
        }
    }
}
