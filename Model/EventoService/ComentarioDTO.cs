using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class ComentarioDTO
    {

        public long idComentario { get; set; }
        public String login { get; set; }
        public DateTime fecha { get; set; }
        public String texto { get; set; }

        public ComentarioDTO() { }

        public ComentarioDTO(ComentarioDTO dto)
        {
            // TODO: Complete member initialization
            this.idComentario = dto.idComentario;
            this.login = dto.login;
            this.fecha = dto.fecha;
            this.texto = dto.texto;
        }



        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            ComentarioDTO p = obj as ComentarioDTO;
            if ((System.Object)p == null)
            {
                return false;
            }

            return ((idComentario == p.idComentario) &&
                     login.Equals(p.login) &&
                     fecha.Equals(p.fecha) &&
                     texto.Equals(p.texto));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
