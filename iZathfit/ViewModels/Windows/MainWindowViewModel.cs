namespace iZathfit.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _applicationTitle = "iZathfit Modo Fit";

    [ObservableProperty]
    bool showButtons = false;

}
