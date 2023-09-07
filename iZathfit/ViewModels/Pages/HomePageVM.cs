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
	readonly localDialogService _dialog;
	public HomePageVM(IGeneralConfiguration config, SettingPage settingPage, Home homesub, localDialogService localDialogService)
	{
		_config = config;
		_settingPage = settingPage;
		_homesub = homesub;
		_dialog = localDialogService;
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

	public void DatoUsuario() {
		var user = _config.getuserSistema();
		Iniciales = user.Nombres.Substring(0, 1).ToUpper() + user.Apellidos.Substring(0, 1).ToUpper();
		if (UserControl is Home)
		{
            Bienvenidatexto = (user?.generoCode == "M" ? "Bienvenido " : "Bienvenida ")
                        + user?.Nombres?.Split(' ')[0] + " " + user?.Apellidos?.Split(' ')[0] + " al Home de iZathFit";
            IconIndicator = SymbolRegular.Home20;
        }
        Personanombre = user?.Nombres?.Split(' ')[0].ToUpper() + " " + user?.Apellidos?.Split(' ')[0].ToUpper();
		Roles = string.Join(", ", user.Roles.Select(x => x.Description));
        

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
				SubItems = new ObservableCollection<SubItemModel>(){ 
					new SubItemModel(){ 
						Title = "Mantenimiento Usuario",
						Icon = SymbolRegular.PersonAccounts24
					},
					new SubItemModel(){ 
						Title = "Mantenimiento de Roles",
						Icon = SymbolRegular.Classification24
					},new
					(){ 
						Title = "Mantenimiento de Personas",
						Icon = SymbolRegular.PersonEdit24
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
	double width = 80;

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
			menupanel.Width = !IsOpen ? 360 : 80;
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
		_dialog.ShowDialog(new Models.ModelsCommons.DialogModel()
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
