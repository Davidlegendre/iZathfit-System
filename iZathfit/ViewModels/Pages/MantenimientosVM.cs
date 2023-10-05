using Configuration;
using Configuration.GlobalHelpers;
using iZathfit.ModelsComponents;
using iZathfit.Views.Pages.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Common;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientosVM : ObservableObject,IDisposable
    {
        IGlobalHelpers? _helpers;
        public MantenimientosVM()
        { 
            _helpers = App.GetService<IGlobalHelpers>();
            Mantenimientolist = new ObservableCollection<MenuItemCardsModel>()
            {
                new MenuItemCardsModel() { Title = "Mantenimiento de Personas",
                    Description = "Acá podrá realizar el manteniento de las personas, ya sea cliente, personal u otro. Puede Agregar, " +
                    "Eliminar, Actualizar y consultar todas las personas (algunos casos tienen restricciones)",
                    Icon = Wpf.Ui.Common.SymbolRegular.PersonEdit24,
                    Comando = () => {
                        Limpiar();
                        ChangeContentInterno(App.GetService<MantenimientoPersonas>(),
                            "Mantenimiento de Personas", Wpf.Ui.Common.SymbolRegular.PersonEdit24);
                    },
                    Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño, TypeRol.Administrador)
                    ? Visibility.Collapsed : Visibility.Visible
                },
                new MenuItemCardsModel()
                {
                    Title = "Mantenimiento de Usuarios",
                    Description = "En esta seccion administrará los usuarios del sistema",
                    Icon = SymbolRegular.LayerDiagonalPerson20,
                    Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño)
                    ? Visibility.Collapsed : Visibility.Visible,
                    Comando = () => {  Limpiar(); ChangeContentInterno(App.GetService<MantenimientoUsuarios>(),
                    "Mantenimiento de Usuario", SymbolRegular.LayerDiagonalPerson20); }
                },
                new MenuItemCardsModel()
                {
                    Title = "Mantenimiento de Ocupaciones",
                    Description = "En esta seccion se administra las ocupaciones que hacen cada uno de sus trabajadores y clientes",
                    Icon = SymbolRegular.ContactCardRibbon20,
                    Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño, TypeRol.Administrador)
                    ? Visibility.Collapsed : Visibility.Visible,
                    Comando = () => { Limpiar(); ChangeContentInterno(App.GetService<MantenimientoOcupaciones>(),
                    "Mantenimiento de Ocupaciones", SymbolRegular.ContactCardRibbon20); }
                },
                new MenuItemCardsModel()
                {
                    Title = "Mantenimiento de Tipo de Identificaciones",
                    Description = "En esta seccion se administra los tipos de identificaciones",
                    Icon = SymbolRegular.Incognito20,
                    Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño)
                    ? Visibility.Collapsed : Visibility.Visible,
                    Comando = () => {  Limpiar(); ChangeContentInterno(App.GetService<MantenimientoTipoIdentificacion>(),
                    "Mantenimiento de Tipo de Identificaciones", SymbolRegular.Incognito20); }
                },
                new MenuItemCardsModel()
                {
                    Title = "Mantenimiento de Padecimientos y Enfermedades",
                    Description = "En esta seccion se administra los Padecimientos y Enfermedades de todas las personas: trabajadores, clientes, etc",
                    Icon = SymbolRegular.Stethoscope20,
                    Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño, TypeRol.Administrador)
                    ? Visibility.Collapsed : Visibility.Visible,
                    Comando = () => {  Limpiar(); ChangeContentInterno(App.GetService<MantenimientoPadecimientosEnfermedades>(),
                    "Mantenimiento de Padecimientos y Enfermedades", SymbolRegular.Stethoscope20); }
                },
                new MenuItemCardsModel()
                {
                    Title = "Mantenimiento de Tipo de Pagos",
                    Description = "En esta seccion se administra los Padecimientos y Enfermedades de todas las personas: trabajadores, clientes, etc",
                    Icon = SymbolRegular. MoneyHand20,
                    Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño)
                    ? Visibility.Collapsed : Visibility.Visible,
                    Comando = () => { Limpiar(); ChangeContentInterno(App.GetService<MantenimientoTipoPago>(),
                    "Mantenimiento de Tipo de Pagos", SymbolRegular.MoneyHand20); }
                }

            };
        }
        [ObservableProperty]
        ObservableCollection<MenuItemCardsModel>? _mantenimientolist;

        [ObservableProperty]
        HorizontalAlignment? _listahorizontalaligment = HorizontalAlignment.Stretch;

        [ObservableProperty]
        string? _titleActualPag = "";

        [ObservableProperty]
        Visibility _visibilityCardWin = Visibility.Collapsed;

        [ObservableProperty]
        UserControl? _contentMantenimientoPag = null;

        [ObservableProperty]
        SymbolRegular _iconActualPag = SymbolRegular.Empty;

        [ObservableProperty]
        bool _enabledMenuMantenimiento = true;


        [ObservableProperty]
        int _columns = 4;

        [RelayCommand]
        void ChangeTONormal() {
            Listahorizontalaligment = HorizontalAlignment.Stretch;
            ContentMantenimientoPag = null;
            VisibilityCardWin = Visibility.Collapsed;
            EnabledMenuMantenimiento = true;
        }

        void ChangeContentInterno(UserControl? user, string titulo, SymbolRegular icon) {
            Listahorizontalaligment = HorizontalAlignment.Stretch;
            ContentMantenimientoPag = user;
            TitleActualPag = titulo;
            IconActualPag = icon;
            VisibilityCardWin = Visibility.Visible;
            EnabledMenuMantenimiento = false;
        }

        void Limpiar() {
            ContentMantenimientoPag?.GetType().GetMethod("Dispose")?.Invoke(ContentMantenimientoPag, null);
        }

        public void Dispose()
        {
            _helpers = null;
            Mantenimientolist = null;
            ContentMantenimientoPag = null;
        }
    }
}
