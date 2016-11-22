using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    [SerializableAttribute]
    public class UsuarioNoPropietarioException : Exception
    {
        /// <summary>
        /// Guarda el id del usuario de la excepcion
        /// </summary>
        /// <value>Identificador del usuario no propietario</value>
        private readonly long _usuarioId;

        /// <summary>
        /// Guarda el id del comentario de la excepcion
        /// </summary>
        /// <value>Identificador del comentario</value>
         private readonly long _comentarioId;

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
            _usuarioId = usuarioId;
            _comentarioId = comentarioId;
        }


        protected UsuarioNoPropietarioException(SerializationInfo info, StreamingContext context) :base(info, context){ }

        public UsuarioNoPropietarioException() :base("El usuario no es el propietario del comentario") { }

        public UsuarioNoPropietarioException(String info) :base("El usuario no es el propietario del comentario: " + info){ }

        public UsuarioNoPropietarioException(String info, Exception exception) : base("El usuario no es el propietario del comentario: " + info) { }
        
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("usuarioId", _usuarioId);
            info.AddValue("comentarioId", _comentarioId);
        }

        

        public long usuarioId
        {
            get { return _usuarioId; }
        }

        public long comentarioId
        {
            get { return _comentarioId; }
        }



    }
}
