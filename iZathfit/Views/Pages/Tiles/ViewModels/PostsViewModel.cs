using Models.ModelsCommons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace iZathfit.Views.Pages.Tiles.ViewModels
{
    public partial class PostsViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string>? _fontslist;

        [ObservableProperty]
        string? _fontFamilySelected;
        

        [ObservableProperty]
        Visibility _visiblePanelEdit = Visibility.Collapsed;

        [ObservableProperty]
        Visibility _visibleTextEdit = Visibility.Visible;

        [ObservableProperty]
        Visibility _visibleEditFontStyle = Visibility.Collapsed;

        [ObservableProperty]
        Visibility _buttonOpenPanel = Visibility.Visible;

        public void CargarFonts() {
            var fonts = Fonts.SystemFontFamilies.Select(x => x.Source);            
            Fontslist = new ObservableCollection<string>(fonts);
            fonts = null;
        }
    }
}
