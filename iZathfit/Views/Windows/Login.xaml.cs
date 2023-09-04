using Commons;
using Models.ModelsCommons;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Windows;

/// <summary>
/// Lógica de interacción para Login.xaml
/// </summary>
public partial class Login : UiWindow
{
    readonly localDialogService localDialogService;
    public Login()
    {
       
        InitializeComponent();
        localDialogService = App.GetService<localDialogService>()!;
        this.Closing += Login_Closing; ;
    }

    private void Login_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        DialogResult = true;
    }

    private void btnclick_Click(object sender, RoutedEventArgs e)
    {
        var result = localDialogService.ShowDialog(new DialogModel() { 
            Title = "Prueba",
            Message = "hola como estan",
            aceptarContent = "Ok",
            //Links = new (){ new() { Url = "http://google.com", TitlePage = "Google" } }

        });
        if (result) {
            localDialogService.ShowDialog(new DialogModel() { Title = "presionado", Message = "boton ok presionado", canShowCancelButton = false });
        }

        this.Close();
    }
}
