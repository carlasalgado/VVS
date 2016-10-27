using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class BloqueGrupos
    {
        public Collection<GrupoDTO> Grupos { get; private set; }
        public bool ExistenMasGrupos { get; private set; }

        public BloqueGrupos(Collection<GrupoDTO> grupos, bool existenMasGrupos){
            this.Grupos = grupos;
            this.ExistenMasGrupos = existenMasGrupos;
        }
    }
}
