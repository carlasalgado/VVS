using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.GroupPages
{
    public partial class MisGrupos : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lblNoGroups.Visible = false;

            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

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

            IGrupoService groupService =
                container.Resolve<IGrupoService>();

            /* Get Groups Info */
            List<GrupoDTO> groups =
                groupService.BuscarPorUsuario(userSession.UserProfileId);

            if (groups.Count == 0)
            {
                lblNoGroups.Visible = true;
                return;
            }

            this.gvGroups.DataSource = groups;
            this.gvGroups.DataBind();
        }


        protected void gvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Accessing BoundField Column

            String groupID = gvGroups.SelectedRow.Cells[0].Text;

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IGrupoService groupService =
                container.Resolve<IGrupoService>();

            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

            /* Get User Identifier passed as parameter in the request from 
             * the previous page 
             */

            groupService.BajaGrupo(userSession.UserProfileId, long.Parse(groupID));

            Response.Redirect("~/Pages/SuccessfulOperation.aspx");

        }
    }
}