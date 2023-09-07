using System.Collections.ObjectModel;
using Wpf.Ui.Common;

namespace iZathfit.ModelsComponents
{
    public class MenuUserItemsModel
    {
        public string? TituloItem { get; set; }
        public SymbolRegular Icon { get; set; }

        public ObservableCollection<SubItemModel>? SubItems { get; set; }
    }
}
