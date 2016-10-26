using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class GrupoDTO
    {

        public long idGrupo { get; set; }
        public String nombre {  get;  set; }
        public String descripcion { get; set; }
        public int nMiembros { get; set; }
        public int nRecomendaciones { get; set; }

        public GrupoDTO() { }

        public GrupoDTO(GrupoDTO dto)
        {
            // TODO: Complete member initialization
            this.idGrupo = dto.idGrupo;
            this.nombre = dto.nombre;
            this.descripcion = dto.descripcion;
            this.nMiembros = dto.nMiembros;
            this.nRecomendaciones = dto.nRecomendaciones;
        }


        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            GrupoDTO p = obj as GrupoDTO;
            if ((System.Object)p == null)
            {
                return false;
            }

            return  ((idGrupo == p.idGrupo) &&
                     nombre.Contains(p.nombre) && 
                     descripcion.Equals(p.descripcion) &&
                    (nMiembros == p.nMiembros) &&
                    (nRecomendaciones == p.nRecomendaciones));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
