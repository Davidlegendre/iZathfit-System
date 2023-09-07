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

namespace iZathfit.Components
{
    /// <summary>
    /// Lógica de interacción para LogoSVGModoFit.xaml
    /// </summary>
    public partial class LogoSVGModoFit : UserControl
    {
        public LogoSVGModoFit()
        {
            InitializeComponent();
            this.Loaded += LogoSVGModoFit_Loaded;
        }

        private void LogoSVGModoFit_Loaded(object sender, RoutedEventArgs e)
        {
            logo.Fill = this.Tag as Brush;
        }
    }
}
