using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.PortableExecutable;

namespace iZathfit.Components.SelectSearchComponent
{
    public partial class SelectSearchViewModel : ObservableObject
    {
        [ObservableProperty]
        Visibility _visibleVacioTexto = Visibility.Visible;
        [ObservableProperty]
        Visibility _visibleContent = Visibility.Collapsed;

        [ObservableProperty]
        string? _header = "";

        [ObservableProperty]
        string? _content = "";

        [ObservableProperty]
        PersonaModel? _personaselected = null;

        [ObservableProperty]
        ContratoModel? _contratoselected = null;

        [ObservableProperty]
        ObservableCollection<PersonaModel> _personas = new ObservableCollection<PersonaModel>();

        [ObservableProperty]
        ObservableCollection<ContratoModel> _contratos = new ObservableCollection<ContratoModel>();

        public void BuscarFuncion(string buscatext)
        {
            if (!string.IsNullOrWhiteSpace(buscatext))
            {
                var resultado = Personas.FirstOrDefault(x => x.Identificacion == buscatext);
                if (resultado != null)
                {
                    Header = resultado.GetCompleteName;
                    Content = resultado.Rol;
                    VisibleContent = Visibility.Visible;
                    VisibleVacioTexto = Visibility.Collapsed;
                    Personaselected = resultado;
                }
                else
                {
                    SetNull(Personaselected);
                }
            }
            else
            {
                SetNull(Personaselected);
            }
        }

        public void BuscarContrato(string buscatext)
        {
            if (!string.IsNullOrWhiteSpace(buscatext))
            {
                var resultado = Contratos.FirstOrDefault(x => x.NumeroContrato == buscatext);
                if (resultado != null)
                {
                    Header = resultado.GetNombreContrato;
                    Content = resultado.ValorTotal.ToString("0.00") + " S/";
                    VisibleContent = Visibility.Visible;
                    VisibleVacioTexto = Visibility.Collapsed;
                    Contratoselected = resultado;
                }
                else
                {
                    SetNull(Contratoselected);
                }

            }
            else
            {
                SetNull(Contratoselected);
            }
        }

        public void SetNull<T>(T? clase) where T : class
        {
            Header = "";
            Content = "";
            VisibleContent = Visibility.Collapsed;
            VisibleVacioTexto = Visibility.Visible;
            clase = null;
        }
    }
}
