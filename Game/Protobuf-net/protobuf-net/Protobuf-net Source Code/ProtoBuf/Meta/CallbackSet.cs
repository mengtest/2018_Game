﻿namespace ProtoBuf.Meta
{
    using ProtoBuf;
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class CallbackSet
    {
        private MethodInfo afterDeserialize;
        private MethodInfo afterSerialize;
        private MethodInfo beforeDeserialize;
        private MethodInfo beforeSerialize;
        private readonly MetaType metaType;

        internal CallbackSet(MetaType metaType)
        {
            if (metaType == null)
            {
                throw new ArgumentNullException("metaType");
            }
            this.metaType = metaType;
        }

        internal static bool CheckCallbackParameters(TypeModel model, MethodInfo method)
        {
            int num2;
            ParameterInfo[] parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i = num2 + 1)
            {
                Type parameterType = parameters[i].ParameterType;
                if (((parameterType != model.MapType(typeof(SerializationContext))) && (parameterType != model.MapType(typeof(Type)))) && (parameterType != model.MapType(typeof(StreamingContext))))
                {
                    return false;
                }
                num2 = i;
            }
            return true;
        }

        internal static Exception CreateInvalidCallbackSignature(MethodInfo method)
        {
            return new NotSupportedException("Invalid callback signature in " + method.DeclaringType.FullName + "." + method.Name);
        }

        private MethodInfo SanityCheckCallback(TypeModel model, MethodInfo callback)
        {
            this.metaType.ThrowIfFrozen();
            if (callback != null)
            {
                if (callback.IsStatic)
                {
                    throw new ArgumentException("Callbacks cannot be static", "callback");
                }
                if ((callback.ReturnType != model.MapType(typeof(void))) || !CheckCallbackParameters(model, callback))
                {
                    throw CreateInvalidCallbackSignature(callback);
                }
            }
            return callback;
        }

        public MethodInfo AfterDeserialize
        {
            get
            {
                return this.afterDeserialize;
            }
            set
            {
                this.afterDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
            }
        }

        public MethodInfo AfterSerialize
        {
            get
            {
                return this.afterSerialize;
            }
            set
            {
                this.afterSerialize = this.SanityCheckCallback(this.metaType.Model, value);
            }
        }

        public MethodInfo BeforeDeserialize
        {
            get
            {
                return this.beforeDeserialize;
            }
            set
            {
                this.beforeDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
            }
        }

        public MethodInfo BeforeSerialize
        {
            get
            {
                return this.beforeSerialize;
            }
            set
            {
                this.beforeSerialize = this.SanityCheckCallback(this.metaType.Model, value);
            }
        }

        internal MethodInfo this[TypeModel.CallbackType callbackType]
        {
            get
            {
                switch (callbackType)
                {
                    case TypeModel.CallbackType.BeforeSerialize:
                        return this.beforeSerialize;

                    case TypeModel.CallbackType.AfterSerialize:
                        return this.afterSerialize;

                    case TypeModel.CallbackType.BeforeDeserialize:
                        return this.beforeDeserialize;

                    case TypeModel.CallbackType.AfterDeserialize:
                        return this.afterDeserialize;
                }
                throw new ArgumentException("Callback type not supported: " + callbackType.ToString(), "callbackType");
            }
        }

        public bool NonTrivial
        {
            get
            {
                return ((((this.beforeSerialize != null) || (this.beforeDeserialize != null)) || (this.afterSerialize != null)) || (this.afterDeserialize > null));
            }
        }
    }
}

