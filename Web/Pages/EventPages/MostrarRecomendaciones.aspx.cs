using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
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
    public partial class MostrarRecomendaciones : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoEvents.Visible = false;

            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];


            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventService =
                container.Resolve<IEventoService>();

            /* Get Groups Info */
            Collection<RecomendacionDTO> recomendaciones =
                eventService.MostrarRecomendaciones(userSession.UserProfileId);

            if (recomendaciones.Count == 0)
            {
                lblNoEvents.Visible = true;
                return;
            }


            this.gvEvents.DataSource = recomendaciones;
            this.gvEvents.DataBind();
        }

        protected void gvEvents_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow selectedRow = gvEvents.Rows[index];

            if (e.CommandName == "Select")
            {
                Response.Redirect("~/Pages/EventPages/VerComentarios.aspx" +
                            "?idEvento=" + selectedRow.Cells[0].Text);
            }
        }
    }
}