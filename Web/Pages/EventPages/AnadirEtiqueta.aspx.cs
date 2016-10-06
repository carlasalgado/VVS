using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class AnadirEtiqueta : SpecificCulturePage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        public long idEvento;
        public long idComentario;

        public string redir;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                idEvento = Int32.Parse(Request.Params.Get("idEvento"));
            }
            catch
            {
                idComentario = Int32.Parse(Request.Params.Get("idComentario"));
            }

            redir = Request.Params.Get("redir");
            lblTagError.Visible = false;
        }

        protected void BtnCreateTagClick(object sender, EventArgs e)
        {
            string direccion =  "~/Pages/EventPages";

            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];

            IEventoService eventoService = container.Resolve<IEventoService>();
            try
            {

                UserSession userSession = (UserSession)Context.Session[USER_SESSION_ATTRIBUTE];

                eventoService.CrearEtiqueta(txtTag.Text);

                switch (redir){
                    case "a":
                        direccion = direccion + "/AnadirComentario.aspx?idEvento=" + idEvento;
                        break;
                    case "m":
                        direccion = direccion + "/ModificarComentario.aspx?idComentario=" + idComentario;
                        break;
                }


                Response.Redirect(Response.
                       ApplyAppPathModifier(direccion));
            }
            catch (DuplicateInstanceException)
            {
                lblTagError.Visible = true;
            }
        }

    }
}