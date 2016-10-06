using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class BloqueEventos
    {

        public List<EventoDTO> Eventos { get; private set; }
        public bool ExistenMasEventos { get; private set; }

        public BloqueEventos(List<EventoDTO> eventos, bool existenMasEventos)
        {
            this.Eventos = eventos;
            this.ExistenMasEventos = existenMasEventos;
        }
    }
}
