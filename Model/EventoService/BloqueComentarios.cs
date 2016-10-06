using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class BloqueComentarios
    {

        public List<ComentarioDTO> Comentarios { get; private set; }
        public bool ExistenMasComentarios { get; private set; }

        public BloqueComentarios(List<ComentarioDTO> comentarios, bool existenMasComentarios)
        {
            this.Comentarios = comentarios;
            this.ExistenMasComentarios = existenMasComentarios;
        }
    }
}
