using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class AnadirComentario : SpecificCulturePage
    {
        public long idEvento;
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            idEvento = Int32.Parse(Request.Params.Get("idEvento"));
            lblNoTags.Visible = false;

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventService =
                container.Resolve<IEventoService>();

            /* Get Tags Info */
            Collection<Etiqueta> etiquetas =
                eventService.NubeEtiquetas();

            if (etiquetas.Count == 0)
            {
                lblNoTags.Visible = true;
                return;
            }
            if (!IsPostBack)
            {
                this.gvEtiquetas.DataSource = etiquetas;
                this.gvEtiquetas.DataBind();
            }
        }

        

        protected void BtnComentarClick(object sender, EventArgs e)
        {
            IUnityContainer container =
              (IUnityContainer)HttpContext.Current.Application["unityContainer"];

            IEventoService eventoService = container.Resolve<IEventoService>();

            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();

            long idComentario = eventoService.AnadirComentario(idEvento, userSession.UserProfileId, txtComentario.Text);
            
            
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
              ApplyAppPathModifier("~/Pages/EventPages/AnadirEtiqueta.aspx?idEvento=" + idEvento + "&redir=a"));
        }
    }
}