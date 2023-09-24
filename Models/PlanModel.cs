using Models.ServiciodeModelos;

namespace Models
{
    public class PlanModel
    {
        public int IdPlanes { get; set; }
        public int IdPlanDuracion { get; set; }
        public Guid IdUsuario { get; set; }
        public string? descripcion { get; set; }
        public decimal Precio { get; set; }
        public int MesesTiempo { get; set; }
        public string? PlanDuracionDescrip { get; set; }

        public List<ServicioModel>? Servicios { get; set; }

        public string? GetPrecioString => Precio.ToString("0.00") + " S/";
        public string? GetTitulo => "Plan paquete de: " + GetMesesTiempoString;
        public string? GetServicios => Servicios != null ? string.Join(", ", Servicios.Select(x => x.NombreServicio)) : "";
        public string GetMesesTiempoString => NotificadorServicesInModels.TransformMonthsToString(MesesTiempo);
    }
}
