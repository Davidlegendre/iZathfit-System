using Models.ModelsCommons;
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
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Commons
{
    /// <summary>
    /// Lógica de interacción para Dialog.xaml
    /// </summary>
    public partial class Dialog : UiWindow
    {
        bool resultdialog = false;
        public Dialog(DialogModel dialogModel)
        {
            InitializeComponent();
            this.Closing += Dialog_Closing;
            DataContext = dialogModel;
            btnClose.Visibility = dialogModel.canShowCancelButton ? Visibility.Visible : Visibility.Collapsed;
            griduni.Columns = dialogModel.canShowCancelButton ? 2 : 1;
            links.Visibility = dialogModel.Links?.Count() > 0 ? Visibility.Visible : Visibility.Collapsed; 
                        
        }

        private void Dialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            this.Close();
        }
    }
}
