using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.Components.ElementosUsuario;
using iZathfit.ModelsComponents;
using iZathfit.Views.Pages.Negocio;
using iZathfit.Views.Pages.Negocio.Ventanas;
using iZathfit.Views.Pages.SubPagesHome;
using iZathfit.Views.Windows;
using Models;
using Models.ModelsCommons;
using Models.ServiciodeModelos;
using Services.Promocion;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages;
public partial class HomePageVM : ObservableObject, IDisposable
{

	

	IGeneralConfiguration? _config;
	IGlobalHelpers? _helpers;
	Home? _homesub;
	localDialogService? _dialog;
	IPromocionService? _servicePromo;
	
	public HomePageVM()
	{
		_config = App.GetService<IGeneralConfiguration>();
		_homesub = App.GetService<Home>();
		_dialog = App.GetService<localDialogService>();
		_helpers = App.GetService<IGlobalHelpers>();
		_servicePromo = App.GetService<IPromocionService>();
		NotificadorServicesInModels.PromoEliminarEvent += NotificadorServicesInModels_PromoEliminarEvent;
		NotificadorServicesInModels.PromosEvent += NotificadorServicesInModels_PromosEvent;
	}

	private void NotificadorServicesInModels_PromoEliminarEvent(object? sender, PromocionModelo e)
	{
		if (Promociones.AsList().Exists(x => x.IdPromocion == e.IdPromocion))
		{
			Promociones.Remove(e);
			IsPromos = Promociones.Count() != 0;
			Apariencia = Promociones.Count() != 0 ? ControlAppearance.Success : ControlAppearance.Secondary;
		}
	}

	private void NotificadorServicesInModels_PromosEvent(object? sender, ObservableCollection<PromocionModelo> e)
	{
		Promociones = new ObservableCollection<PromocionModelo>(e.AsList().Where(x => x.DuracionTiempo.Date >= DateTime.Now.Date));
		IsPromos = e.Count() != 0;
		Apariencia = e.Count() != 0 ? ControlAppearance.Success : ControlAppearance.Secondary;
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
	string? _email = "";

	[ObservableProperty]
	UserControl? _userControl = null;

	[ObservableProperty]
	string? _bienvenidatexto = "";

	[ObservableProperty]
	string? _iniciales = "";

	[ObservableProperty]
	double? _heightButtonItemMenu = 48;

	[ObservableProperty]
	string? _versionApp = "4.1.7";

	[ObservableProperty]
	string? _titleProgram = "iZathFit [Modo Fit]";

	[ObservableProperty]
	string? _copyRigth = "CopyRigth iZathFit 2023";

	[ObservableProperty]
	LinkModel[] _links;	

	[ObservableProperty]
	SymbolRegular _iconAccount = SymbolRegular.DeveloperBoard20;


	void ChangeDatosPerfil() {
        var user = _config?.getuserSistema();
        Iniciales = user.Nombres.Substring(0, 1).ToUpper() + user.Apellidos.Substring(0, 1).ToUpper();
        Personanombre = user?.Nombres?.Split(' ')[0].ToUpper() + " " + user?.Apellidos?.Split(' ')[0].ToUpper();
        Email = user?.Email;
		
    }

	async void DatoUsuario() {
        var user = _config?.getuserSistema();
        if (UserControl is Home)
		{
			Bienvenidatexto = (user?.generoCode == "M" ? "Bienvenido " : "Bienvenida ")
						+ user?.Nombres?.Split(' ')[0] + " " + user?.Apellidos?.Split(' ')[0] + " al Home de iZathFit";
			IconIndicator = SymbolRegular.Home20;
		}
		
		Ocupaciones = user?.Ocupacion;
		Rol = user?.Rol;		

		var promos = await _servicePromo.GetPromocionesActive();
		if(promos != null)
		NotificadorServicesInModels.NotificarPromos(new ObservableCollection<PromocionModelo>(promos));

		if (user.CodeRol == _config.GetRol(TypeRol.Desarrollador)) IconAccount = SymbolRegular.DeveloperBoard20;
		else if (user.CodeRol == _config.GetRol(TypeRol.Administrador)) IconAccount = SymbolRegular.ShieldPerson20;
		else if (user.CodeRol == _config.GetRol(typerol: TypeRol.Dueño)) IconAccount = SymbolRegular.PersonSettings20;
		else IconAccount = SymbolRegular.Person20;

		Links = _helpers.GetLinksContacts();
    }

	public void CargarDatos() {
        var user = _config?.getuserSistema();
        UserControl = _homesub;
		ChangeDatosPerfil();
        DatoUsuario();
        Menuitems = new ObservableCollection<MenuUserItemsModel>() {
			new MenuUserItemsModel(){
				TituloItem = "Cambiar Contraseña",
				Icon = SymbolRegular.Password20,
                Comando = () => {
                        var contraseñawin = new CambiarContraseñaWin();
                        contraseñawin.ShowDialog();
                    }
            },
			new MenuUserItemsModel()
			{
				TituloItem = "Cambiar Datos Personales",
				Icon = SymbolRegular.Book20,
				Comando = () => {
						var userdatawin = new DatosPerfinWindow(user);
						userdatawin.ChangeUserAction = () => ChangeDatosPerfil();
						userdatawin.ShowDialog();
					}				
			}
		};

		Menulist = new ObservableCollection<MenuUserItemsModel>() {
			new MenuUserItemsModel()
			{
				TituloItem = "Agregar Cliente",
				Icon = SymbolRegular.PeopleCommunity24,
				Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Administrador) ? Visibility.Collapsed : Visibility.Visible,
				Comando = () => {
				new WizardCliente().ShowDialog();

                }
            },
            new MenuUserItemsModel()
            {
                TituloItem = "Agregar Rutina",
                Icon = SymbolRegular.PersonRunning20,
                Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Administrador) ? Visibility.Collapsed : Visibility.Visible,
				Comando = () => {
                    new Rutina().ShowDialog();					
				}
            }
            ,new(){
				TituloItem = "Mantenimientos",
				Icon = SymbolRegular.EditSettings24,
				Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño) ? Visibility.Collapsed : Visibility.Visible,
                Comando = () => {
					if(UserControl is not MantenimientosPage)
					{
						Liberar();
						UserControl = App.GetService<MantenimientosPage>();
						ChangeIndicator(SymbolRegular.EditSettings24, "Esta en Mantenimientos");
					}
				}
			},
			new(){
				TituloItem = "Administracion de Servicios",
				Icon = SymbolRegular.ServiceBell20,
                Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño) ? Visibility.Collapsed : Visibility.Visible,
                Comando = () => {
					if(UserControl is not ServiciosPage)
					{
                        Liberar();
                        UserControl = App.GetService<ServiciosPage>();
						ChangeIndicator(SymbolRegular.ServiceBell20, "Esta en Administracion de Servicios");
					}
				}
			},
			new(){
				TituloItem = "Administracion de Duraciones de los Planes",
				Icon = SymbolRegular.ClockToolbox20,
                Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño) ? Visibility.Collapsed : Visibility.Visible,
                Comando = () => {
					if(UserControl is not PlanDuracionPage)
					{
                        Liberar();
                        UserControl = App.GetService<PlanDuracionPage>();
						ChangeIndicator(SymbolRegular.ClockToolbox20, "Esta en Administracion de Duraciones de los Planes");
					}
				}
			},
			new(){
				TituloItem = "Administracion de Planes",
				Icon = SymbolRegular.Box24,
                Visible = !_helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño) ? Visibility.Collapsed : Visibility.Visible,
                Comando = () => {
					if(UserControl is not PlanesPage)
					{
                        Liberar();
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
                        Liberar();
                        UserControl = App.GetService<PromocionesPage>();
                        ChangeIndicator(SymbolRegular.ShoppingBagPercent20, "Administracion de Promociones");
                    }
                }
            },
            new(){
                TituloItem = "Administracion de Contratos",
                Icon = SymbolRegular.ReceiptCube24,
                Comando = () => {
                    if(UserControl is not Contratos)
                    {
                        Liberar();
                        UserControl = App.GetService<Contratos>();
                        ChangeIndicator(SymbolRegular.ReceiptCube24, "Esta en Administracion de Contratos");
                    }
                }
            },
            new(){
                TituloItem = "Administracion de Pagos",
                Icon = SymbolRegular.MoneyHand20,
                Comando = () => {
                    if(UserControl is not PagosPage)
                    {
                        Liberar();
                        UserControl = App.GetService<PagosPage>();
                        ChangeIndicator(SymbolRegular.MoneyHand20, "Esta en Administracion de Pagos");
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
		if (_homesub != null && _homesub.vm != null)
		{
			_homesub.vm.ViewRelojPanelCommand.Execute(null);
		}
        DatoUsuario();
		ButtonHome = Visibility.Collapsed;
		App.GetService<MainWindow>()?.Alzeimer();
		
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
			links: _helpers?.GetLinksContacts(), owner: App.GetService<MainWindow>());
	}

	void ChangeIndicator(SymbolRegular icon, string texto)
	{
        IconIndicator = icon;
        Bienvenidatexto = texto;
        ButtonHome = Visibility.Visible;
    }


	void Liberar() {
		UserControl?.GetType().GetMethod("Dispose")?.Invoke(UserControl, null);
		App.GetService<MainWindow>()?.Alzeimer();
    }

    public void Dispose()
    {
		Liberar();
		_config = null;
		_homesub = null;
		_dialog = null;
        _helpers = null;
        _servicePromo = null;
		Promociones = null;
		Menuitems = null;
		Menulist = null;
		UserControl = null;
    }
}
