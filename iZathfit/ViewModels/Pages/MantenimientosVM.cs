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
    public partial class MantenimientosVM : ObservableObject
    {
        public MantenimientosVM()
        { 
            Mantenimientolist = new ObservableCollection<MenuItemCardsModel>()
            {
                new MenuItemCardsModel() { Title = "Mantenimiento de Personas",
                    Description = "Acá podrá realizar el manteniento de las personas, ya sea cliente, personal u otro. Puede Agregar, " +
                    "Eliminar, Actualizar y consultar todas las personas (algunos casos tienen restricciones)",
                    Icon = Wpf.Ui.Common.SymbolRegular.PersonEdit24,
                    Comando = () => {
                        ChangeContentInterno(App.GetService<MantenimientoPersonas>(), 
                            "Mantenimiento de Personas", Wpf.Ui.Common.SymbolRegular.PersonEdit24);
                    }
                },
                new MenuItemCardsModel()
                {
                    Title = "Mantenimiento de Ventanas",
                    Description = "Acá puede administrar las ventanas del sistema, es para poder usarlos en los permisos posteriormente",
                    Icon = SymbolRegular.WindowWrench24
                }

            };
        }
        [ObservableProperty]
        ObservableCollection<MenuItemCardsModel>? mantenimientolist;

        [ObservableProperty]
        HorizontalAlignment? listahorizontalaligment = HorizontalAlignment.Stretch;

        [ObservableProperty]
        string? titleActualPag = "";

        [ObservableProperty]
        Visibility visibilityCardWin = Visibility.Collapsed;

        [ObservableProperty]
        UserControl? contentMantenimientoPag = null;

        [ObservableProperty]
        SymbolRegular iconActualPag = SymbolRegular.Empty;

        [ObservableProperty]
        bool enabledMenuMantenimiento = true;

        [RelayCommand]
        void ChangeTONormal() {
            Listahorizontalaligment = HorizontalAlignment.Stretch;
            ContentMantenimientoPag = null;
            VisibilityCardWin = Visibility.Collapsed;
            EnabledMenuMantenimiento = true;
        }

        void ChangeContentInterno(UserControl? user, string titulo, SymbolRegular icon) {
            Listahorizontalaligment = HorizontalAlignment.Left;
            ContentMantenimientoPag = user;
            TitleActualPag = titulo;
            IconActualPag = icon;
            VisibilityCardWin = Visibility.Visible;
            EnabledMenuMantenimiento = false;
        }

       
    }
}
