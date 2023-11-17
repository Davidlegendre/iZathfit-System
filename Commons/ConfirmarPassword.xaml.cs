using Models.ModelsCommons;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace Commons
{
    /// <summary>
    /// Lógica de interacción para ConfirmarPassword.xaml
    /// </summary>
    public partial class ConfirmarPassword : UiWindow
    {
        ConfirmPasswordModel model;
        bool resultdialog = false;
       
        public ConfirmarPassword(ConfirmPasswordModel dialogModel)
        {
            InitializeComponent();
            model = dialogModel;
            DataContext = dialogModel;
            this.Loaded += ConfirmarPassword_Loaded;
            this.Closing += ConfirmarPassword_Closing;
        }

        private void ConfirmarPassword_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private void ConfirmarPassword_Loaded(object sender, RoutedEventArgs e)
        {
            txtconfirmcontrasena.Focus();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            aceptar();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            this.Close();
        }

        private void mensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                aceptar();
            }
            else if (e.Key == Key.Escape)
            {
                resultdialog = false;
                this.Close();
            }
        }

        void aceptar()
        {
            var contra = EncryptManagementService.EncryptManagementService.Encrypt(txtconfirmcontrasena.Password);
            if (string.IsNullOrWhiteSpace(contra)
                   || contra != model.Contrasena)
            {
                resultdialog = false;
                this.Close();
                return;
            }

            resultdialog = true;
            this.Close();
        }
    }
}
