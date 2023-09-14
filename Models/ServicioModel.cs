using System.ComponentModel;

namespace Models
{
    public class ServicioModel : INotifyPropertyChanged
    {
        bool _isselected = false;
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFin { get; set; }
        public bool? IsGrupal { get; set; }
        public Guid IdUsuario { get; set; }
        public string GetHorarioFin => HorarioFin.ToShortTimeString();
        public string GetHorarioInicio => HorarioInicio.ToShortTimeString();
        public bool IsSelected {
            get => _isselected;
            set { 
                if(value != _isselected)
                {
                    _isselected = value;
                    change(nameof(IsSelected));
                }
            }
        }
        public string GetIsGrupalInString => IsGrupal == true ? "Grupal" : "No es Grupal";

        void change(string propiedad)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
