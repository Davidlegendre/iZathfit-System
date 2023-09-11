using Configuration;
using Configuration.GlobalHelpers;
using iZathfit.ModelsComponents;
using iZathfit.Views.Pages;
using iZathfit.Views.Pages.SubPagesHome;
using iZathfit.Views.Windows;
using Microsoft.Windows.Themes;
using Models;
using Services.Genero;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Windows.Controls;
using System.Windows.Media;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Services;

namespace iZathfit.ViewModels.Pages;
public partial class HomePageVM : ObservableObject {

	IGeneralConfiguration? _config;
	SettingPage? _settingPage;
	Home? _homesub;
	localDialogService? _dialog;
	public HomePageVM()
	{
		_config = App.GetService<IGeneralConfiguration>();
		_settingPage = App.GetService<SettingPage>();
		_homesub = App.GetService<Home>();
		_dialog = App.GetService<localDialogService>();
	}

	[ObservableProperty]
	string? personanombre = "";

	[ObservableProperty]
	string? ocupaciones = "";
	[ObservableProperty]
	string? rol = "";

	[ObservableProperty]
	UserControl? userControl = null;

	[ObservableProperty]
	string? bienvenidatexto = "";

	[ObservableProperty]
	string? iniciales = "";

	[ObservableProperty]
	SymbolRegular iconAccount = SymbolRegular.DeveloperBoard20;

	public void DatoUsuario() {
		var user = _config?.getuserSistema();
		Iniciales = user.Nombres.Substring(0, 1).ToUpper() + user.Apellidos.Substring(0, 1).ToUpper();
		if (UserControl is Home)
		{
			Bienvenidatexto = (user?.generoCode == "M" ? "Bienvenido " : "Bienvenida ")
						+ user?.Nombres?.Split(' ')[0] + " " + user?.Apellidos?.Split(' ')[0] + " al Home de iZathFit";
			IconIndicator = SymbolRegular.Home20;
		}
		Personanombre = user?.Nombres?.Split(' ')[0].ToUpper() + " " + user?.Apellidos?.Split(' ')[0].ToUpper();
		Ocupaciones = string.Join(", ", user.Ocupaciones.Select(x => x.Description));
		Rol = user.Rol;

		if (user.CodeRol == _config.GetRol(TypeRol.Desarrollador)) IconAccount = SymbolRegular.DeveloperBoard20;
		else if (user.CodeRol == _config.GetRol(TypeRol.Administrador)) IconAccount = SymbolRegular.ShieldPerson20;
		else if (user.CodeRol == _config.GetRol(typerol: TypeRol.Dueño)) IconAccount = SymbolRegular.PersonSettings20;
		else IconAccount = SymbolRegular.Person20;
    }

	public void CargarDatos() {		
		
		UserControl = _homesub;
        DatoUsuario();
        Menuitems = new ObservableCollection<MenuUserItemsModel>() {
			new MenuUserItemsModel(){
				TituloItem = "Cambiar Contraseña",
				Icon = SymbolRegular.Password20,

			},
			new MenuUserItemsModel()
			{
				TituloItem = "Cambiar Datos Personales",
				Icon = SymbolRegular.Book20
			},
			new MenuUserItemsModel(){
				TituloItem = "Acerca de",
				Icon = SymbolRegular.Info20
			}
		};

		Menulist = new ObservableCollection<MenuUserItemsModel>() {
			new(){
				TituloItem = "Mantenimientos",
				Icon = SymbolRegular.EditSettings24,
				Comando = () => {
					if(UserControl is not MantenimientosPage)
					{
						UserControl = App.GetService<MantenimientosPage>();
						ChangeIndicator(SymbolRegular.EditSettings24, "Esta en Mantenimientos");
					}
				}
			}
        };
	}

	[ObservableProperty]
	ObservableCollection<MenuUserItemsModel>? menuitems;

	[ObservableProperty]
	ObservableCollection<MenuUserItemsModel>? menulist;

	[ObservableProperty]
	bool isOpen = false;

	[ObservableProperty]
	Visibility buttonHome = Visibility.Collapsed;

	[ObservableProperty]
	double width = 48;

	[ObservableProperty]
	Thickness marginiconPerfil;

	[ObservableProperty]
	SymbolRegular iconmenu = SymbolRegular.LineHorizontal320;

	[ObservableProperty]
	SymbolRegular iconIndicator = SymbolRegular.Home20;


	[RelayCommand]
	void cerrarSesion() {
		App.GetService<MainWindow>()?.Close();

	}

	[RelayCommand]
	void volverHome() {
        UserControl = _homesub;
        DatoUsuario();
		ButtonHome = Visibility.Collapsed;
		
    }

	[RelayCommand]
	void OpenMenu(Card? menupanel) {
		if (menupanel is not null)
		{            
			Wpf.Ui.Animations.Transitions.ApplyTransition(menupanel, 
				!IsOpen ? Wpf.Ui.Animations.TransitionType.FadeIn
				: Wpf.Ui.Animations.TransitionType.FadeIn,
				700);
			menupanel.Width = !IsOpen ? 270 : 48;
			IsOpen = !IsOpen;
        }
	}

	[RelayCommand]
	void openSettingsPage()
	{
		UserControl = _settingPage;
		ChangeIndicator(SymbolRegular.Settings20, "Estas en Configuraciones");
	}

	[RelayCommand]
	void ReportBug() {
		_dialog?.ShowDialog(new Models.ModelsCommons.DialogModel()
		{
			Title = "Desea Reportar un Bug?",
			Message = "Usted puede contactar con los siguientes enlaces",
			canShowCancelButton = false,
			Links = new List<Models.ModelsCommons.LinkModel>() {
				new Models.ModelsCommons.LinkModel(){
					TitlePage = "Whatsapp Dev. 1",
					Url = "https://google.com"
                },
				new Models.ModelsCommons.LinkModel()
				{
					TitlePage = "Whatsapp Dev. 2...",
					Url = "https://google.com"
                }
				,new Models.ModelsCommons.LinkModel()
                {
                    TitlePage = "Reportar a GitHub",
                    Url = "https://github.com/Davidlegendre/iZathfit-System"
                }
            }
		}, App.GetService<MainWindow>());
	}

	void ChangeIndicator(SymbolRegular icon, string texto)
	{
        IconIndicator = icon;
        Bienvenidatexto = texto;
        ButtonHome = Visibility.Visible;
    }


	
	


}
