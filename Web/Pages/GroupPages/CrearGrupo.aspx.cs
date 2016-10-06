using Es.Udc.DotNet.PracticaMaD.Model.GrupoService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.GroupPages
{
    public partial class CrearGrupo : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void BtnCreateGroupClick(object sender, EventArgs e)
        {
            IUnityContainer container =
              (IUnityContainer)HttpContext.Current.Application["unityContainer"];

            IGrupoService groupService = container.Resolve<IGrupoService>();
            try
            {

                UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

                Grupo group = new Grupo();
                group.nombre = txtGroupName.Text;
                group.descripcion = txtGroupDescription.Text;

                groupService.CrearGrupo(group, userSession.UserProfileId);

                Response.Redirect(Response.
                       ApplyAppPathModifier("~/Pages/SuccessfulOperation.aspx"));
            }
            catch (DuplicateInstanceException)
            {
                lblGroupNameError.Visible = true;
            }

        }
    }
}