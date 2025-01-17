﻿namespace ProtoBuf.Serializers
{
    using ProtoBuf;
    using ProtoBuf.Compiler;
    using ProtoBuf.Meta;
    using System;

    internal sealed class Int32Serializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof(int);

        public Int32Serializer(TypeModel model)
        {
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadInt32", this.ExpectedType);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteInt32", valueFrom);
        }

        public object Read(object value, ProtoReader source)
        {
            Helpers.DebugAssert(value == null);
            return source.ReadInt32();
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteInt32((int) value, dest);
        }

        public Type ExpectedType
        {
            get
            {
                return expectedType;
            }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get
            {
                return false;
            }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get
            {
                return true;
            }
        }
    }
}

