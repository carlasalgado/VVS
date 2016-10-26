using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
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

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class RecomendarEvento : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";


        long idEvento;
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            idEvento = Int32.Parse(Request.Params.Get("idEvento")); ;


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

            IEventoService eventService =
                container.Resolve<IEventoService>();

            if (!IsPostBack)
            {
                /* Get Groups Info */
                List<GrupoDTO> aux =
                    groupService.BuscarPorUsuario(userSession.UserProfileId);

                List<GrupoDTO> grupos = new List<GrupoDTO>();

                foreach (GrupoDTO g in aux)
                    if (!eventService.GrupoRecomendado(idEvento, g.idGrupo))
                        grupos.Add(g);

                if (grupos.Count == 0)
                {
                    lblNoGroups.Visible = true;
                    txtTxtRecomendacion.Visible = false;
                    btnRecommendGroup.Visible = false;
                    lclTxtRecomendacion.Visible = false;
                    return;
                }

                this.gvGroups.DataSource = grupos;
                this.gvGroups.DataBind();
            }
        }




        protected void BtnRecommendClick(object sender, EventArgs e)
        {
            List<Grupo> grupos = new List<Grupo>();

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventoService =
                container.Resolve<IEventoService>();

            IGrupoService grupoService =
                container.Resolve<IGrupoService>();

            UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];


            foreach (GridViewRow row in gvGroups.Rows)
            {
                //CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                CheckBox check = (CheckBox)row.Cells[5].FindControl("chkSeleccion");
                if (check.Checked)
                {

                    grupos.Add(grupoService.BuscarUnGrupo(Int32.Parse(row.Cells[0].Text)));

                }

            }
            if (grupos.Count == 0)
            {
                Response.Redirect("~/Pages/EventPages/SinGrupos.aspx");
            }
            else
            {
                eventoService.RecomendarEvento(idEvento, grupos, txtTxtRecomendacion.Text);
                Response.Redirect("~/Pages/SuccessfulOperation.aspx");
            }


        }
    }
}