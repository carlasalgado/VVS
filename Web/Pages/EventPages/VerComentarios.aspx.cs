using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class VerComentarios : SpecificCulturePage
    {
        long eventId;
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            eventId = Int32.Parse(Request.Params.Get("idEvento"));
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }


            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventService =
                container.Resolve<IEventoService>();


            EventoDTO evento = eventService.BuscarEvento(eventId);

            lblnombreEvento.Text = evento.nombre;
            lblfechaEvento.Text = evento.fecha.ToString();
            lblreseñaEvento.Text = evento.reseña;

            /* Get Comment Info */
            BloqueComentarios commentBlock =
                eventService.VerComentarios(eventId, startIndex, count);

            if ((commentBlock.Comentarios.Count == 0) && (startIndex == 0))
            {
                lblNoComments.Visible = true;
                return;
            }

            this.gvComments.DataSource = commentBlock.Comentarios;
            this.gvComments.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/EventPages/VerComentarios.aspx" + "?idEvento=" + eventId +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistenMasComentarios)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/EventPages/VerComentarios.aspx" + "?idEvento=" + eventId +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }


            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

            try
            {
                if (userSession.UserProfileId != 0)
                {
                    List<ComentarioDTO> comentarios = eventService.BuscarComentarioPorUsuario(userSession.UserProfileId, eventId);
                    ComentarioDTO c = null;

                    for (int i = 0; i < commentBlock.Comentarios.Count; i++)
                    {
                        c = commentBlock.Comentarios[i];

                        if (!comentarios.Contains(c))
                        {
                            gvComments.Rows[i].Cells[5].Text = "";
                            gvComments.Rows[i].Cells[6].Text = "";
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                gvComments.Columns[5].Visible = false;
                gvComments.Columns[6].Visible = false;
            }
        }

        protected void BtnComentarClick(object sender, EventArgs e) {
            Response.Redirect(Response.
                      ApplyAppPathModifier("~/Pages/EventPages/AnadirComentario.aspx" +
                      "?idEvento=" + eventId));
        }

        protected void gvComments_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);


            GridViewRow selectedRow = gvComments.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    Response.Redirect(Response.
                       ApplyAppPathModifier("~/Pages/EventPages/ModificarComentario.aspx" +
                       "?idComentario=" + selectedRow.Cells[0].Text));
                    break;

                case "Ver":

                    Response.Redirect(Response.
                       ApplyAppPathModifier("~/Pages/EventPages/VerComentario.aspx" +
                       "?idComentario=" + selectedRow.Cells[0].Text + "&idEvento=" + eventId));
                    break;

                case "Eliminar":

                    UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

                    if (!SessionManager.IsUserAuthenticated(Context))
                    {
                        Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                    }
                    /* Get the Service */
                    IUnityContainer container =
                    (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

                    IEventoService eventService =
                    container.Resolve<IEventoService>();

                    eventService.EliminarComentario(Int32.Parse(selectedRow.Cells[0].Text), userSession.UserProfileId);

                    Response.Redirect(Response.
                         ApplyAppPathModifier("~/Pages/EventPages/VerComentarios.aspx" +
                         "?idEvento=" + eventId));
                    break;
            }
        }

    }
}