using System;

namespace Es.Udc.DotNet.ModelUtil.Exceptions
{    
    public abstract class InstanceException : ModelException
    {
        protected InstanceException(String specificMessage, Object key,
            String className)
            : base(specificMessage + " (key = '" + key +
                                    "' - className = '" + className + "')")
        {

            this.Key = key;
            this.ClassName = className;

        }

        #region Properties

        public Object Key { get; private set; }

        public string ClassName { get; private set; }

        #endregion

    }
}
