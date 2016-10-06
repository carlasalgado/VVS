using Es.Udc.DotNet.PracticaMaD.Model;
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
    public partial class VerComentariosEtiqueta : SpecificCulturePage
    {
        String tag;
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            tag = Request.Params.Get("tag");
            int startIndex, count;

        
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

            /* Get Comment Info */
            List<ComentarioDTO> comentarios =
                eventService.MostrarComentariosEtiqueta(tag);

            if ((comentarios.Count == 0) && (startIndex == 0))
            {
                lblNoComments.Visible = true;
                return;
            }

            this.gvComments.DataSource = comentarios;
            this.gvComments.DataBind();

            lblTag.Text = tag;
        }

    }
}