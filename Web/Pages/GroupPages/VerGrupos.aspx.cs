using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
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
    public partial class VerGrupos : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
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
            BloqueGrupos groupBlock =
                groupService.VerGrupos(startIndex, count);



            if (groupBlock.Grupos.Count == 0)
            {
                lblNoGroups.Visible = true;
                return;
            }

            this.gvGroups.DataSource = groupBlock.Grupos;
            this.gvGroups.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/GroupPages/VerGrupos.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (groupBlock.ExistenMasGrupos)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/GroupPages/VerGrupos.aspx" +
                    "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

            try
            {
                if (userSession.UserProfileId != 0)
                {
                    List<GrupoDTO> groups = groupService.BuscarPorUsuario(userSession.UserProfileId);
                    GrupoDTO g = null;
                    int a = gvGroups.Rows.Count;
                    for (int i = 0; i < groupBlock.Grupos.Count; i++)
                    {
                        g = groupService.BuscarGrupo(groupBlock.Grupos[i].idGrupo);

                        if (groups.Contains(g))
                            gvGroups.Rows[i].Cells[5].Visible = false;
                    }
                }
            }
            catch (NullReferenceException)
            {
                gvGroups.Columns[5].Visible = false;
            }
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
            groupService.AltaGrupo(userSession.UserProfileId, long.Parse(groupID));

            Response.Redirect("~/Pages/SuccessfulOperation.aspx");
        }
    }
}