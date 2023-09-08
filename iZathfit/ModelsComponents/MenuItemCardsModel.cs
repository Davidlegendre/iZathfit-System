using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;

namespace iZathfit.ModelsComponents
{
    public class MenuItemCardsModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public SymbolRegular Icon { get; set; }
        public Action? Comando { get; set; }
    }
}
