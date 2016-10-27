using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class BloqueComentarios
    {

        public Collection<ComentarioDTO> Comentarios { get; private set; }
        public bool ExistenMasComentarios { get; private set; }

        public BloqueComentarios(Collection<ComentarioDTO> comentarios, bool existenMasComentarios)
        {
            this.Comentarios = comentarios;
            this.ExistenMasComentarios = existenMasComentarios;
        }
    }
}
