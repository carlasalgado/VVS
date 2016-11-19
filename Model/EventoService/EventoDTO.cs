using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class EventoDTO
    {
        public long idEvento { get; set; }
        public String nombre { get; set; }
        public String reseña { get; set; }

        public DateTime fecha { get; set; }

        public EventoDTO() { }

        public EventoDTO(EventoDTO dto)
        {
            if (dto != null)
            {
                this.idEvento = dto.idEvento;
                this.nombre = dto.nombre;
                this.reseña = dto.reseña;
                this.fecha = dto.fecha;
            }
        }


        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            EventoDTO p = obj as EventoDTO;
            if ((System.Object)p == null)
            {
                return false;
            }

            return ((idEvento == p.idEvento) &&
                     nombre.Equals(p.nombre) &&
                     reseña.Equals(p.reseña) &&
                     fecha.Equals(p.fecha));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
