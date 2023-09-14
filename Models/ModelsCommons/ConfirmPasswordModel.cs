using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Models.ModelsCommons
{
    public class ConfirmPasswordModel
    {
        public string Title { get; set; }
        
        public string? Message { get; set; }
        public string Contrasena { get; set; }

    }
}
