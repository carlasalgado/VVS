using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages
{
    public partial class BuscarEventos : SpecificCulturePage
    {

        protected void BtnBuscarClick(object sender, EventArgs e)
        {
            String url = String.Format("./MostrarEventos.aspx?keywords={0}", this.txtkeyword.Text);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}