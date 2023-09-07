using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;

namespace iZathfit.ModelsComponents
{
    public class SubItemModel
    {
        public string? Title { get; set; }
        public SymbolRegular Icon { get; set; }
        public Action? commando { get; set; }

        
    }
}
