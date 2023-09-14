using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
using Models;
using Services.TipoIdentificacion;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios
{
    /// <summary>
    /// Lógica de interacción para MTipoIdentifiacionForm.xaml
    /// </summary>
    public partial class MTipoIdentifiacionForm : UiWindow
    {
        TipoIdentificacionModel? _tipoidenty;
        MTIpoIdentificacionVM? _Vm;
        localDialogService? _dialog;
        bool resultdialog = false;
        ObservableCollection<TipoIdentificacionModel> _lista;
        public MTipoIdentifiacionForm(ObservableCollection<TipoIdentificacionModel> lista,TipoIdentificacionModel? tipoidenty = null)
        {
            InitializeComponent();
            _tipoidenty = tipoidenty;
            _Vm = this.DataContext as MTIpoIdentificacionVM;
            _dialog = App.GetService<localDialogService>();
            _lista = lista;
            this.Loaded += MTipoIdentifiacionForm_Loaded;
            this.Closing += MTipoIdentifiacionForm_Closing;
        }

        private void MTipoIdentifiacionForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private void MTipoIdentifiacionForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Vm != null) _Vm.Cargar(_tipoidenty);
            TBTitulo.Title = _tipoidenty == null ? "Agregar" : "Modificar";
            txtTipoIdenty.Focus();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_Vm != null)
            {
                if (_dialog?.ShowDialog("Desea Guardar el tipo de identificacion?", ShowCancelButton: true) == true)
                {
                    var result = _tipoidenty == null ? await _Vm.Guardar(_lista, this) :
                        await _Vm.Update(_lista, this, _tipoidenty.IdTipoIdentity);
                    if (result)
                    {
                        resultdialog = true;
                        this.Close();
                    }
                }
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            this.Close();
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (_Vm != null)
            {
                _Vm.limpiar();
                txtTipoIdenty.Focus();
            }
        }
    }
}
