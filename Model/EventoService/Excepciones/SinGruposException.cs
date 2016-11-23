using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    [SerializableAttribute]
    public class SinGruposException : Exception
    {
          public SinGruposException()
      {
         // Add any type-specific logic, and supply the default message.
      }

      public SinGruposException(string message): base(message) 
      {
         // Add any type-specific logic.
      }
      public SinGruposException(string message, Exception innerException): 
         base (message, innerException)
      {
         // Add any type-specific logic for inner exceptions.
      }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SinGruposException"/> class.
        /// </summary>
        protected SinGruposException(SerializationInfo info, 
         StreamingContext context)  : base(info, context){ }
      
           
    }
}
