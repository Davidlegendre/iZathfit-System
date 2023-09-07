using iZathfit.ViewModels.Windows;
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
using Wpf.Ui.Controls;

namespace iZathfit.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para AcercaDe.xaml
    /// </summary>
    public partial class AcercaDe : UiWindow
    {
        readonly AcercadeVM _vm;
        public AcercaDe(AcercadeVM vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = vm;
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
