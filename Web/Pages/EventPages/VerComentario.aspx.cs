using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class VerComentario : System.Web.UI.Page
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        public long comentarioId;
        public long eventoId;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoTags.Visible = false;

            comentarioId = Int32.Parse(Request.Params.Get("idComentario"));
            eventoId = Int32.Parse(Request.Params.Get("idEvento"));

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventService =
                container.Resolve<IEventoService>();

            /* Get Tag Info */
            List<Etiqueta> etiquetas =
                eventService.EtiquetasDeComentario(comentarioId);

            EventoDTO eventoDto = eventService.BuscarEvento(eventoId);
            ComentarioDTO comentarioDto = eventService.BuscarComentario(comentarioId);

            lblNombreEvento.Text = eventoDto.nombre;
            lblFechaEvento.Text = eventoDto.fecha.ToString();
            lblReseñaEvento.Text = eventoDto.reseña;

            lblLogin.Text = comentarioDto.login;
            lblFechaComentario.Text = comentarioDto.fecha.ToString();
            lblComentario.Text = comentarioDto.texto;

            if (etiquetas.Count == 0)
            {
                lblNoTags.Visible = true;
                return;
            }

            this.gvEtiquetas.DataSource = etiquetas;
            this.gvEtiquetas.DataBind();


        }
    }
}