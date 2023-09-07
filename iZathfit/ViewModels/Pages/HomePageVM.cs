using Configuration;
using iZathfit.ModelsComponents;
using iZathfit.Views.Pages;
using iZathfit.Views.Pages.SubPagesHome;
using iZathfit.Views.Windows;
using Models;
using Services.Genero;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages;
public partial class HomePageVM : ObservableObject {

	readonly IGeneralConfiguration _config;
	readonly SettingPage _settingPage;
	readonly Home _homesub;
	public HomePageVM(IGeneralConfiguration config, SettingPage settingPage, Home homesub)
	{
		_config = config;
		_settingPage = settingPage;
		_homesub = homesub;
	}
	[ObservableProperty]
	string? personanombre = "";

	[ObservableProperty]
	string? roles = "";

	[ObservableProperty]
	UserControl? userControl = null;

	[ObservableProperty]
	string? bienvenidatexto = "";

	[ObservableProperty]
	string? iniciales = "";

	void DatoUsuario() {
		var user = _config.getuserSistema();
		Iniciales = user.Nombres.Substring(0, 1).ToUpper() + user.Apellidos.Substring(0, 1).ToUpper();
		Bienvenidatexto = (user?.generoCode == "M" ? "Bienvenido " : "Bienvenida ") + user?.Nombres?.Split(' ')[0] + " " + user?.Apellidos?.Split(' ')[0] + " al Home de iZathFit";
		Personanombre = user?.Nombres?.Split(' ')[0].ToUpper() + " " + user?.Apellidos?.Split(' ')[0].ToUpper();
		Roles = string.Join(", ", user.Roles.Select(x => x.Description));
		IconIndicator = SymbolRegular.Home20;
	}

	public void CargarDatos() {

		DatoUsuario();
		UserControl = _homesub;
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

		Menu = new ObservableCollection<MenuUserItemsModel>() {
			new(){
				TituloItem = "Mantenimientos",
				Icon = SymbolRegular.EditSettings24,
				Comando = () =>
				{
					ChangeIndicator(SymbolRegular.EditSettings24, "Estas en Mantenimientos");
				}
			},
			new(){
				TituloItem = "Registrar Contrato",
				Icon = SymbolRegular.DocumentQueue20,
				Comando = () =>
				{
                    ChangeIndicator(SymbolRegular.DocumentQueue20, "Estas en los registros de Contrato");
                }
			}
		};
	}

	[ObservableProperty]
	ObservableCollection<MenuUserItemsModel>? menuitems;

	[ObservableProperty]
	ObservableCollection<MenuUserItemsModel>? menu;

	[ObservableProperty]
	bool isOpen = false;

	[ObservableProperty]
	Visibility buttonHome = Visibility.Collapsed;

	[ObservableProperty]
	double width = 68;

	[ObservableProperty]
	Thickness marginiconPerfil;

	[ObservableProperty]
	SymbolRegular iconmenu = SymbolRegular.LineHorizontal320;

	[ObservableProperty]
	SymbolRegular iconIndicator = SymbolRegular.Home20;

	[RelayCommand]
	void cerrarSesion() {
		App.GetService<MainWindow>().Close();

	}

	[RelayCommand]
	void volverHome() {
		DatoUsuario();
		ButtonHome = Visibility.Collapsed;
		UserControl = _homesub;
    }

	[RelayCommand]
	void OpenMenu(Card? menupanel) {
		if (menupanel is not null)
		{            
			Wpf.Ui.Animations.Transitions.ApplyTransition(menupanel, 
				!IsOpen ? Wpf.Ui.Animations.TransitionType.FadeIn
				: Wpf.Ui.Animations.TransitionType.FadeIn,
				700);
			menupanel.Width = !IsOpen ? 360 : 48;
			IsOpen = !IsOpen;
        }
	}

	[RelayCommand]
	void openSettingsPage()
	{
		UserControl = _settingPage;
		ChangeIndicator(SymbolRegular.Settings20, "Estas en Configuraciones");
	}


	void ChangeIndicator(SymbolRegular icon, string texto)
	{
        IconIndicator = icon;
        Bienvenidatexto = texto;
        ButtonHome = Visibility.Visible;
    }
	


}
