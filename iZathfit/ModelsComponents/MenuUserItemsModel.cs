﻿using Wpf.Ui.Common;

namespace iZathfit.ModelsComponents
{
    public class MenuUserItemsModel
    {
        public string? TituloItem { get; set; }
        public SymbolRegular Icon { get; set; }
        public Action? Comando { get; set; }
    }
}
