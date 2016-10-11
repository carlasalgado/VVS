using System;


namespace Es.Udc.DotNet.ModelUtil.Exceptions
{

    public class InstanceNotFoundException : InstanceException
    {

        public InstanceNotFoundException(Object key, String className)
            : base("Instance not found", key, className) { }

    }
}
