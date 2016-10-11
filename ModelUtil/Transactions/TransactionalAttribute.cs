using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Es.Udc.DotNet.ModelUtil.Transactions
{
    public class TransactionalAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TransactionalHandler();
        }
    }
}
