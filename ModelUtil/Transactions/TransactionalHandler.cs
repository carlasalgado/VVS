using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Log;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Es.Udc.DotNet.ModelUtil.Transactions
{

    /// <summary>
    /// Defines a wrapper method to encapsulate a method inside a transaction
    /// </summary>
    /// <remarks>
    /// Requires "Microsoft Distributed Transaction Coordinator" service started
    /// </remarks>
    /// <see cref="http://msdn.microsoft.com/en-us/library/dd140045.aspx"/>
    public class TransactionalHandler : ICallHandler
    {
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input,
            GetNextHandlerDelegate getNext)
        {
            IMethodReturn result;

            string methodName = input.MethodBase.ToString();
            string targetClass = input.Target.GetType().Name;
            
            LogManager.RecordMessage("Transactional interception starts for method " + 
                methodName + " from " + targetClass + " ...", MessageType.Info, "Transaction");

            using (TransactionScope transaction = new TransactionScope())
            {

                result = getNext().Invoke(input, getNext);

                transaction.Complete();
            }

            LogManager.RecordMessage("Transactional interception ends...", MessageType.Info, "Transaction");

            return result;
        }
    }
}
