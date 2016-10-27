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
using System.Runtime.Caching;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class MostrarEventos : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String keywords = Request.Params.Get("keywords");
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoEvents.Visible = false;

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

            MemoryCache cache = new MemoryCache("ProveedorCache");

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventService =
                container.Resolve<IEventoService>();

            /* Get Groups Info */
            BloqueEventos eventBlock =
                eventService.BusquedaEventos(keywords,0,0);

            if ((eventBlock.Eventos.Count == 0) && (startIndex == 0))
            {
                lblNoEvents.Visible = true;
                return;
            }

            this.gvEvents.DataSource = eventBlock.Eventos;
            this.gvEvents.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/EventPages/MostrarEventos.aspx" + "?keywords=" + keywords +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (eventBlock.ExistenMasEventos)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/EventPages/MostrarEventos.aspx" + "?keywords=" + keywords +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }


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
            if (e.CommandName == "Insert")
            {
                Response.Redirect("~/Pages/EventPages/RecomendarEvento.aspx" +
                    "?idEvento=" + selectedRow.Cells[0].Text);
            }   
        }
    }
}