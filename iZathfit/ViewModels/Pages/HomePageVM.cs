using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.ModelsComponents;
using iZathfit.Views.Pages;
using iZathfit.Views.Pages.Negocio;
using iZathfit.Views.Pages.SubPagesHome;
using iZathfit.Views.Windows;
using Models;
using Services;
using Services.Promocion;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Controls;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages;
public partial class HomePageVM : ObservableObject {

	IGeneralConfiguration? _config;
	IGlobalHelpers? _helpers;
	GlobalService? _globalservice;
	Home? _homesub;
	localDialogService? _dialog;
	IPromocionService? _servicePromo;
	public HomePageVM()
	{
		_config = App.GetService<IGeneralConfiguration>();
		_homesub = App.GetService<Home>();
		_dialog = App.GetService<localDialogService>();
		_helpers = App.GetService<IGlobalHelpers>();
		_globalservice = App.GetService<GlobalService>();
		_servicePromo = App.GetService<IPromocionService>();
        _globalservice.PromosEvent += _globalservice_PromosEvent;
	}

    private async void _globalservice_PromosEvent(object? sender, object? e)
    {

        if (_servicePromo != null)
        {
            var promos = await _servicePromo.GetPromocionesActive();
            Promociones = promos != null ? new ObservableCollection<PromocionModelo>(promos) : null;
            IsPromos = promos != null;
            Apariencia = promos != null ? ControlAppearance.Success : ControlAppearance.Secondary;
        }
    }

	[ObservableProperty]
	ObservableCollection<PromocionModelo>? _promociones = new ObservableCollection<PromocionModelo>();

	[ObservableProperty]
	bool _isPromos = false;

	[ObservableProperty]
	Wpf.Ui.Common.ControlAppearance _apariencia = ControlAppearance.Secondary;

    [ObservableProperty]
	string? _personanombre = "";

	[ObservableProperty]
	string? _ocupaciones = "";
	[ObservableProperty]
	string? _rol = "";

	[ObservableProperty]
	UserControl? _userControl = null;

	[ObservableProperty]
	string? _bienvenidatexto = "";

	[ObservableProperty]
	string? _iniciales = "";

	[ObservableProperty]
	double? _heightButtonItemMenu = 48;


	

	[ObservableProperty]
	SymbolRegular _iconAccount = SymbolRegular.DeveloperBoard20;

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
		Ocupaciones = user?.Ocupacion;
		Rol = user?.Rol;

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
			},
			new(){
				TituloItem = "Administracion de Servicios",
				Icon = SymbolRegular.ServiceBell20,
				Comando = () => {
					if(UserControl is not ServiciosPage)
					{
						UserControl = App.GetService<ServiciosPage>();
						ChangeIndicator(SymbolRegular.ServiceBell20, "Esta en Administracion de Servicios");
					}
				}
			},
			new(){
				TituloItem = "Administracion de Duraciones de los Planes",
				Icon = SymbolRegular.ClockToolbox20,
				Comando = () => {
					if(UserControl is not PlanDuracionPage)
					{
						UserControl = App.GetService<PlanDuracionPage>();
						ChangeIndicator(SymbolRegular.ClockToolbox20, "Esta en Administracion de Duraciones de los Planes");
					}
				}
			},
			new(){
				TituloItem = "Administracion de Planes",
				Icon = SymbolRegular.Box24,
				Comando = () => {
					if(UserControl is not PlanesPage)
					{
						UserControl = App.GetService<PlanesPage>();
						ChangeIndicator(SymbolRegular.Box24, "Esta en Administracion de Planes");
					}
				}
			},
			new(){
				TituloItem = "Administracion de Promociones",
				Icon = SymbolRegular.ShoppingBagPercent20,
				Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño) ? Visibility.Collapsed:Visibility.Visible,
                Comando = () => {
                    if(UserControl is not PromocionesPage)
                    {
                        UserControl = App.GetService<PromocionesPage>();
                        ChangeIndicator(SymbolRegular.ShoppingBagPercent20, "Administracion de Promociones");
                    }
                }
            },
            new(){
                TituloItem = "Administracion de Contratos",
                Icon = SymbolRegular.ReceiptCube24,
                Comando = () => {
                    if(UserControl is not MantenimientosPage)
                    {
                        UserControl = App.GetService<MantenimientosPage>();
                        ChangeIndicator(SymbolRegular.ReceiptCube24, "Esta en Administracion de Contratos");
                    }
                }
            }
        };
	}

	[ObservableProperty]
	ObservableCollection<MenuUserItemsModel>? _menuitems;

	[ObservableProperty]
	ObservableCollection<MenuUserItemsModel>? _menulist;

	[ObservableProperty]
	bool _isOpen = false;

	[ObservableProperty]
	Visibility _buttonHome = Visibility.Collapsed;

	[ObservableProperty]
	double _width = 48;

	[ObservableProperty]
	Thickness _marginiconPerfil;

	[ObservableProperty]
	SymbolRegular _iconmenu = SymbolRegular.LineHorizontal320;

	[ObservableProperty]
	SymbolRegular _iconIndicator = SymbolRegular.Home20;


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
			HeightButtonItemMenu = !IsOpen ? 48 : null;
        }
	}

	[RelayCommand]
	void OpenPanelNotificacion(Flyout panelnotify)
	{
		panelnotify.Show();

	}

	[RelayCommand]
	void ReportBug()
	{
		_dialog?.ShowDialog(
			titulo: "Desea Reportar un Bug?",
			mensaje: "Usted puede contactar con los siguientes enlaces",
			links: new Models.ModelsCommons.LinkModel[]
			{
				new Models.ModelsCommons.LinkModel()
				{
					TitlePage = "Whatsapp Dev. David",
					Url = "https://api.whatsapp.com/send?phone=51914847720"
                },
				new Models.ModelsCommons.LinkModel()
				{
					TitlePage = "Whatsapp Dev. Francois",
					Url = "https://api.whatsapp.com/send?phone=51998440211"
                }
				, new Models.ModelsCommons.LinkModel()
				{
					TitlePage = "Reportar a GitHub",
					Url = "https://github.com/Davidlegendre/iZathfit-System"
				}
			}, owner: App.GetService<MainWindow>());
	}

	void ChangeIndicator(SymbolRegular icon, string texto)
	{
        IconIndicator = icon;
        Bienvenidatexto = texto;
        ButtonHome = Visibility.Visible;
    }


	
	


}
