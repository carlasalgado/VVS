using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [SerializableAttribute]
    public class IncorrectPasswordException : Exception
    {
        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        private readonly String _loginName;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectPasswordException"/> class.
        /// </summary>
        /// <param name="loginName"><c>loginName</c> that causes the error.</param>
        public IncorrectPasswordException(String loginName)
            : base("Incorrect password exception => loginName = " + loginName)
        {
            _loginName = loginName;
        }

        public IncorrectPasswordException(string loginName, Exception exception) 
                        : base("Incorrect password exception => loginName = " + loginName + "Exception: " + ((exception!= null)?exception.Message:""))
        {}


        protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) :base(info, context){ }

        public IncorrectPasswordException()
            :base("Incorret password expcetion.")
        {
         
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("loginName", _loginName);
        }

        public string loginName
        {
            get { return _loginName; }
        }


    }
}
