using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class TotalesByDateModel
    {
        public string? Nombres { get; set; }
        public decimal Total { get; set; }

        public string? GetTotal => Total.ToString("0.00") + " S/";

    }
}
