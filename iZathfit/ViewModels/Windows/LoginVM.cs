using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models.DTOS;
using Services.Login;
using Services.Persona;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json.Nodes;
using System.Text;
using Wpf.Ui.Controls;
using Models.ModelsCommons;
using Newtonsoft.Json;
using Services.Usuario;

namespace iZathfit.ViewModels.Windows;

public partial class LoginVM : ObservableObject
{
    localDialogService? localDialogService;
    ILoginService? loginService;
    IExceptionHelperService? _helperex;
    IPersonaService? _personaservice;
    IHttpClientFactory? _factoryclient;
    IUsuarioService? _usuario;
    CryptoService? _crypto;
    public LoginVM()
    {
        localDialogService = App.GetService<localDialogService>();
        loginService = App.GetService<ILoginService>();
        _helperex = App.GetService<IExceptionHelperService>();  
        _personaservice = App.GetService<IPersonaService>();
        _factoryclient= App.GetService<IHttpClientFactory>();
        _crypto = App.GetService<CryptoService>();
        _usuario= App.GetService<IUsuarioService>();
    }

    [RelayCommand]
    void close()
    {            
        App.GetService<MainWindow>()?.Close();
    }

    [ObservableProperty]
    string? codeEmail = "";

    [ObservableProperty]
    bool enableEmailtxt = true;

    [ObservableProperty]
    bool enableCodeTXT = false;

    [ObservableProperty]
    bool enableNewPasswordTxt = false;

    [ObservableProperty]
    Guid? guidPersonForgot = null;

    public async Task verificarusuario(string? User, string? Password)
    {
        if(_helperex == null || loginService== null) return;
        if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password))
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Login Incorrecto",
                canShowCancelButton = false,
                Message = "Usuario o Password incorrecto"
            }, App.GetService<MainWindow>());
            return;
        }

        var user = await _helperex.ExcepHandler(() => loginService.Login(User, Password), App.GetService<MainWindow>());

        if (user == null)
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Login Incorrecto",
                canShowCancelButton = false,
                Message = "Usuario o Password incorrecto"
            }, App.GetService<MainWindow>());
            return;
        }

        if (UsuarioLogeado != null)
            UsuarioLogeado.Invoke(this, user);

    }

    public void clean(TextBox txtuser, PasswordBox txtpass) {
        txtuser.Clear();
        txtpass.Clear();
        txtuser.Focus();
    }
    [RelayCommand]
    async Task VerificarEmailPersona(string? email)
    { 
        if(_helperex == null || _personaservice == null) return;

        if(string.IsNullOrWhiteSpace(email))
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel() { Title = "Email Incorrecto", Message = "Ingrese un email",
            canShowCancelButton = false}, App.GetService<MainWindow>());
            return;
        }
        var existemail = await _helperex.ExcepHandler(() => _personaservice.VerificarEmail(email), App.GetService<MainWindow>());

        if (existemail == null) {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Email Incorrecto",
                Message = "Ese email no fue encontrado",
                canShowCancelButton = false
            }, App.GetService<MainWindow>());
            return;
        }
        GuidPersonForgot = existemail;
        var code = _crypto?.GetHashString(email + DateTime.Now + new Random().Next(1,99));
        using(var client = _factoryclient?.CreateClient())
        {
            var mail = new EmailModelApi() {
                body = "Mensaje desde iZathFit, alguien ha pedido restaurar contraseña, por favor ingresa este codigo: " + code,
                isHTMLBody = false,
                subject = "Forgot Password iZathFit",
                toUser = new List<ToUser>() { 
                    new ToUser()
                    {
                        email = email,
                        nombre = "Client iZathFit"
                    }
                }
            };
            var json = JsonConvert.SerializeObject(mail);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("https://mailservice-xgyv-dev.fl0.io/api/Mail/send", content);
            if (result.IsSuccessStatusCode)
            {
                CodeEmail = code;
                EnableCodeTXT= true;
                EnableEmailtxt = false;
            }
        }

        //envio email
    }

    [RelayCommand]
    void VerificarCodigo(string? code) {
        if (code != CodeEmail)
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Codigo Incorrecto",
                Message = "Ingrese el codigo correcto",
                canShowCancelButton = false
            }, App.GetService<MainWindow>());
            return;
        }

        EnableNewPasswordTxt = true;
        EnableCodeTXT = false;
    }

    [RelayCommand]
    async Task GuardarContraseña(string? contraseña) {
        if (_helperex == null || _usuario == null) return;
        if (string.IsNullOrWhiteSpace(contraseña))
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Contraseña Incorrecta",
                Message = "Ingrese una contraseña",
                canShowCancelButton = false
            }, App.GetService<MainWindow>());
            return;
        }

        if (string.IsNullOrWhiteSpace(contraseña))
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Contraseña Incorrecta",
                Message = "Ingrese una contraseña",
                canShowCancelButton = false
            }, App.GetService<MainWindow>());
            return;
        }

        var result = await _helperex.ExcepHandler(() => _usuario.CambiarContraseña(contraseña, GuidPersonForgot), App.GetService<MainWindow>());
        if (result > 0)
        {
            localDialogService?.ShowDialog(new Models.ModelsCommons.DialogModel()
            {
                Title = "Contraseña Cambiada",
                Message = "Su Contraseña ha sido cambiada",
                canShowCancelButton = false
            }, App.GetService<MainWindow>());
            EnableNewPasswordTxt = false;

        }


    }



    public event EventHandler<UsuarioSistema>? UsuarioLogeado;

}