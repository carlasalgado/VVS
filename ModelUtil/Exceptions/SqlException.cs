using System.Data.Common;

namespace Es.Udc.DotNet.ModelUtil.Exceptions
{

    public class SqlException : DbException
    {

        public SqlException(string msg)
            : base(msg) { }

    }

}
