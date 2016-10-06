using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GrupoService
{
    public class BloqueGrupos
    {
        public List<GrupoDTO> Grupos { get; private set; }
        public bool ExistenMasGrupos { get; private set; }

        public BloqueGrupos(List<GrupoDTO> grupos, bool existenMasGrupos){
            this.Grupos = grupos;
            this.ExistenMasGrupos = existenMasGrupos;
        }
    }
}
