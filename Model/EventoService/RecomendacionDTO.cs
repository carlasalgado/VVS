using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class RecomendacionDTO
    {
        public long idEvento { get; set; }
        public String nombre { get; set; }
        public String reseña { get; set; }
        public String recomendacion { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaRecomendacion { get; set; }


        public RecomendacionDTO() { }

        public RecomendacionDTO(RecomendacionDTO dto)
        {
            if (dto != null)
            {
                this.idEvento = dto.idEvento;
                this.nombre = dto.nombre;
                this.reseña = dto.reseña;
                this.recomendacion = dto.recomendacion;
                this.fecha = dto.fecha;
                this.fechaRecomendacion = dto.fechaRecomendacion;
            }
        }


        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            RecomendacionDTO p = obj as RecomendacionDTO;
            if ((System.Object)p == null)
            {
                return false;
            }

            return ((idEvento == p.idEvento) &&
                     nombre.Equals(p.nombre) &&
                     reseña.Equals(p.reseña) &&
                     recomendacion.Equals(p.recomendacion) &&
                     fechaRecomendacion.Equals(p.fechaRecomendacion) &&
                     fecha.Equals(p.fecha));
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
