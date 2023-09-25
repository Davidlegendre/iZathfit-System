
using Models.ServiciodeModelos;
using System.ComponentModel;

namespace Models;

public class PromocionModelo : INotifyPropertyChanged
{
    public PromocionModelo()
    {
        NotificadorServicesInModels.TimeEvent += _notify_TimeEvent;
    }

    private void _notify_TimeEvent(object? sender, EventArgs e)
    {
        change(nameof(GetFaltaDias));
    }

    public int IdPromocion { get; set; }
    public int IdPlan { get; set; }
    public Guid IdUsuario { get; set; }
    public int DescuentoPercent { get; set; }
    public bool IsActivo { get; set; }
    public DateTime DuracionTiempo { get; set; }
    public string? descripcion { get; set; }
    public decimal Precio { get; set; }
    public int MesesTiempo { get; set; }



    public string? GetValidDatetime => "Solo valido hasta el: " + DuracionTiempo.ToLongDateString();
    public string? GetFaltaDias
    {
        get {
            var tiempo = (DuracionTiempo.Date - DateTime.Now.Date).Days;
            if (tiempo < 0) {
                NotificadorServicesInModels.NotificarEliminaciondePromo(this);
                NotificadorServicesInModels.TimeEvent -= _notify_TimeEvent;
            }
           
            return tiempo < 0 ? "Promocion Vencida" : "Faltan: " + tiempo +" dias";
        }
    }
    public decimal GetTotalDescuento => Decimal.Round(Precio - (Convert.ToDecimal(DescuentoPercent) / 100) * Precio, 2);
    public string? GetTotalEnDescuento => (Precio - (Convert.ToDecimal(DescuentoPercent) / 100) * Precio).ToString("0.00") + " S/";
    public string? GetDescuento => DescuentoPercent.ToString() + "%";
    public string? GetPrecioString => Precio.ToString("0.00") + " S/";
    public string? GetTitulo => "Promocion de paquete de: " + GetMesesTiempoString;
    public string GetMesesTiempoString => NotificadorServicesInModels.TransformMonthsToString(MesesTiempo);

    void change(string property)
    {
        if (PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
