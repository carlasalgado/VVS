//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recomendacion
    {
        public Recomendacion()
        {
            this.Grupo = new HashSet<Grupo>();
        }
    
        public long idRecomendacion { get; set; }
        public long idEvento { get; set; }
        public string texto { get; set; }
        public System.DateTime fecha { get; set; }
    
        public Evento Evento { get; set; }
        public ICollection<Grupo> Grupo { get; set; }
    }
}
