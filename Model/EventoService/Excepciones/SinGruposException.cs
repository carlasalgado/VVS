using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class SinGruposException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SinGruposException"/> class.
        /// </summary>
        public SinGruposException()
            : base("La lista de grupos para recomendar el evento está vacía"){ }
    }
}
