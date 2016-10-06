using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class UsuarioNoPropietarioException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UsuarioNoPropietario"/> class.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <param name="comentarioId">Identificador del comentario</param>
        public UsuarioNoPropietarioException(long usuarioId, long comentarioId)
            : base("El usuario con id:" +
            "idUsuario = " + usuarioId + " no es propietario del comentario"
            + " idComentario= " + comentarioId)
        {
            this.usuarioId = usuarioId;
            this.comentarioId = comentarioId;
        }

        /// <summary>
        /// Guarda el id del usuario de la excepcion
        /// </summary>
        /// <value>Identificador del usuario no propietario</value>
        public long usuarioId { get; private set; }

        /// <summary>
        /// Guarda el id del comentario de la excepcion
        /// </summary>
        /// <value>Identificador del comentario</value>
        public long comentarioId { get; private set; }

    }
}
