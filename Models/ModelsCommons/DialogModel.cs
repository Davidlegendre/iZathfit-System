using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Models.ModelsCommons
{
    public class DialogModel
    {
        public string Title { get; set; }
        
        public string? Message { get; set; }
        public string? aceptarContent { get; set; } = "Aceptar";
        public string? cancelarContent { get; set; } = "Cancelar";
        
        public bool canShowCancelButton { get; set; } = true;

        public List<LinkModel>? Links { get; set; }

    }
}
