
namespace Models;

public class PromocionModelo
{
    
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
    public string? GetFaltaDias => "Faltan: " + (DuracionTiempo.Date - DateTime.Now.Date).Days + " dias";

    public string? GetTotalEnDescuento => (Precio - (Convert.ToDecimal(DescuentoPercent) / 100) * Precio).ToString("0.00") + " S/";
    public string? GetDescuento => DescuentoPercent.ToString() + "%";
    public string? GetPrecioString => Precio.ToString("0.00") + " S/";
    public string? GetTitulo => "Promocion de paquete de: " + GetMesesTiempoString;
    public string GetMesesTiempoString => MesesTiempo >= 12 ? "1 Año" : (MesesTiempo == 1) ? "1 Mes" : MesesTiempo + " Meses";
  
}
