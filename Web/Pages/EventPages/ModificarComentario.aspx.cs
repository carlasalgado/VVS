using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class ModificarComentario : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        public long idComentario;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoTags.Visible = false;

            idComentario = Int32.Parse(Request.Params.Get("idComentario"));
            
            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventService =
                container.Resolve<IEventoService>();

            if (!IsPostBack)
            {
                /* Get Tag Info */
                Collection<Etiqueta> etiquetas =
                    eventService.NubeEtiquetas();

                if (etiquetas.Count == 0)
                {
                    lblNoTags.Visible = true;
                    return;
                }

                this.gvEtiquetas.DataSource = etiquetas;
                this.gvEtiquetas.DataBind();

                Collection<Etiqueta> etiquetasComentario = eventService.EtiquetasDeComentario(idComentario);

                foreach (GridViewRow row in gvEtiquetas.Rows)
                {
                   if(etiquetasComentario.Contains( eventService.EtiquetaPorId(Int32.Parse(row.Cells[0].Text)))){
                       CheckBox checkbox = (CheckBox)row.Cells[2].FindControl("chkSeleccion");
                       checkbox.Checked = true;
                   }
                }

                ComentarioDTO comentarioDto = eventService.BuscarComentario(idComentario);

                txtTexto.Text = comentarioDto.texto;
            }

        }

        protected void BtnModificarComentarioClick(object sender, EventArgs e)
        {
            IUnityContainer container =
              (IUnityContainer)HttpContext.Current.Application["unityContainer"];

            IEventoService eventoService = container.Resolve<IEventoService>();

            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

            try
            {
                eventoService.ModificarComentario(idComentario, userSession.UserProfileId, txtTexto.Text);
            }
            catch (UsuarioNoPropietarioException){
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/Errors/UsuarioNoAutorizadoException.aspx"));
            }

            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();

            foreach (GridViewRow row in gvEtiquetas.Rows)
            {
                //CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                CheckBox check = (CheckBox)row.Cells[2].FindControl("chkSeleccion");
                if (check.Checked)
                {
                    etiquetas.Add(eventoService.EtiquetaPorId(Int32.Parse(row.Cells[0].Text)));
                }
            }

            eventoService.AnadirEtiqueta(idComentario, etiquetas);
            Response.Redirect(Response.
                ApplyAppPathModifier("~/Pages/SuccessfulOperation.aspx"));

        }

        protected void BtnAnadirEtiquetaClick(object sender, EventArgs e)
        {
            Response.Redirect(Response.
              ApplyAppPathModifier("~/Pages/EventPages/AnadirEtiqueta.aspx?idComentario=" + idComentario + "&redir=m"));
        }
    }
}