﻿namespace ProtoBuf.Serializers
{
    using ProtoBuf;
    using ProtoBuf.Compiler;
    using ProtoBuf.Meta;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    internal sealed class ArrayDecorator : ProtoDecoratorBase
    {
        private readonly Type arrayType;
        private readonly int fieldNumber;
        private readonly Type itemType;
        private readonly byte options;
        private const byte OPTIONS_OverwriteList = 2;
        private const byte OPTIONS_SupportNull = 4;
        private const byte OPTIONS_WritePacked = 1;
        private readonly WireType packedWireType;

        public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull) : base(tail)
        {
            Helpers.DebugAssert(arrayType > null, "arrayType should be non-null");
            Helpers.DebugAssert(arrayType.IsArray && (arrayType.GetArrayRank() == 1), "should be single-dimension array; " + arrayType.FullName);
            this.itemType = arrayType.GetElementType();
            Type type = supportNull ? this.itemType : (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType);
            Helpers.DebugAssert(type == base.Tail.ExpectedType, "invalid tail");
            Helpers.DebugAssert(base.Tail.ExpectedType != model.MapType(typeof(byte)), "Should have used BlobSerializer");
            if ((writePacked || (packedWireType != WireType.None)) && (fieldNumber <= 0))
            {
                throw new ArgumentOutOfRangeException("fieldNumber");
            }
            if (!ListDecorator.CanPack(packedWireType))
            {
                if (writePacked)
                {
                    throw new InvalidOperationException("Only simple data-types can use packed encoding");
                }
                packedWireType = WireType.None;
            }
            this.fieldNumber = fieldNumber;
            this.packedWireType = packedWireType;
            if (writePacked)
            {
                this.options = (byte) (this.options | 1);
            }
            if (overwriteList)
            {
                this.options = (byte) (this.options | 2);
            }
            if (supportNull)
            {
                this.options = (byte) (this.options | 4);
            }
            this.arrayType = arrayType;
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            Type[] typeArguments = new Type[] { this.itemType };
            Type type = ctx.MapType(typeof(List<>)).MakeGenericType(typeArguments);
            Type expectedType = this.ExpectedType;
            using (Local local = this.AppendToCollection ? ctx.GetLocalWithValue(expectedType, valueFrom) : null)
            {
                using (Local local2 = new Local(ctx, expectedType))
                {
                    using (Local local3 = new Local(ctx, type))
                    {
                        ctx.EmitCtor(type);
                        ctx.StoreValue(local3);
                        ListDecorator.EmitReadList(ctx, local3, base.Tail, type.GetMethod("Add"), this.packedWireType, false);
                        using (Local local4 = this.AppendToCollection ? new Local(ctx, ctx.MapType(typeof(int))) : null)
                        {
                            Type[] types = new Type[] { ctx.MapType(typeof(Array)), ctx.MapType(typeof(int)) };
                            if (this.AppendToCollection)
                            {
                                ctx.LoadLength(local, true);
                                ctx.CopyValue();
                                ctx.StoreValue(local4);
                                ctx.LoadAddress(local3, type);
                                ctx.LoadValue(type.GetProperty("Count"));
                                ctx.Add();
                                ctx.CreateArray(this.itemType, null);
                                ctx.StoreValue(local2);
                                ctx.LoadValue(local4);
                                CodeLabel label = ctx.DefineLabel();
                                ctx.BranchIfFalse(label, true);
                                ctx.LoadValue(local);
                                ctx.LoadValue(local2);
                                ctx.LoadValue(0);
                                ctx.EmitCall(expectedType.GetMethod("CopyTo", types));
                                ctx.MarkLabel(label);
                                ctx.LoadValue(local3);
                                ctx.LoadValue(local2);
                                ctx.LoadValue(local4);
                            }
                            else
                            {
                                ctx.LoadAddress(local3, type);
                                ctx.LoadValue(type.GetProperty("Count"));
                                ctx.CreateArray(this.itemType, null);
                                ctx.StoreValue(local2);
                                ctx.LoadAddress(local3, type);
                                ctx.LoadValue(local2);
                                ctx.LoadValue(0);
                            }
                            types[0] = expectedType;
                            MethodInfo method = type.GetMethod("CopyTo", types);
                            if (method == null)
                            {
                                types[1] = ctx.MapType(typeof(Array));
                                method = type.GetMethod("CopyTo", types);
                            }
                            ctx.EmitCall(method);
                        }
                        ctx.LoadValue(local2);
                    }
                }
            }
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            using (Local local = ctx.GetLocalWithValue(this.arrayType, valueFrom))
            {
                using (Local local2 = new Local(ctx, ctx.MapType(typeof(int))))
                {
                    bool flag = (this.options & 1) > 0;
                    using (Local local3 = flag ? new Local(ctx, ctx.MapType(typeof(SubItemToken))) : null)
                    {
                        Type type = ctx.MapType(typeof(ProtoWriter));
                        if (flag)
                        {
                            ctx.LoadValue(this.fieldNumber);
                            ctx.LoadValue(2);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("WriteFieldHeader"));
                            ctx.LoadValue(local);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("StartSubItem"));
                            ctx.StoreValue(local3);
                            ctx.LoadValue(this.fieldNumber);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("SetPackedField"));
                        }
                        this.EmitWriteArrayLoop(ctx, local2, local);
                        if (flag)
                        {
                            ctx.LoadValue(local3);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("EndSubItem"));
                        }
                    }
                }
            }
        }

        private void EmitWriteArrayLoop(CompilerContext ctx, Local i, Local arr)
        {
            ctx.LoadValue(0);
            ctx.StoreValue(i);
            CodeLabel label = ctx.DefineLabel();
            CodeLabel label2 = ctx.DefineLabel();
            ctx.Branch(label, false);
            ctx.MarkLabel(label2);
            ctx.LoadArrayValue(arr, i);
            if (this.SupportNull)
            {
                base.Tail.EmitWrite(ctx, null);
            }
            else
            {
                ctx.WriteNullCheckedTail(this.itemType, base.Tail, null);
            }
            ctx.LoadValue(i);
            ctx.LoadValue(1);
            ctx.Add();
            ctx.StoreValue(i);
            ctx.MarkLabel(label);
            ctx.LoadValue(i);
            ctx.LoadLength(arr, false);
            ctx.BranchIfLess(label2, false);
        }

        public override object Read(object value, ProtoReader source)
        {
            int fieldNumber = source.FieldNumber;
            BasicList list = new BasicList();
            if ((this.packedWireType != WireType.None) && (source.WireType == WireType.String))
            {
                SubItemToken token = ProtoReader.StartSubItem(source);
                while (ProtoReader.HasSubValue(this.packedWireType, source))
                {
                    list.Add(base.Tail.Read(null, source));
                }
                ProtoReader.EndSubItem(token, source);
            }
            else
            {
                do
                {
                    list.Add(base.Tail.Read(null, source));
                }
                while (source.TryReadFieldHeader(fieldNumber));
            }
            int offset = this.AppendToCollection ? ((value == null) ? 0 : ((Array) value).Length) : 0;
            Array array = Array.CreateInstance(this.itemType, (int) (offset + list.Count));
            if (offset > 0)
            {
                ((Array) value).CopyTo(array, 0);
            }
            list.CopyTo(array, offset);
            return array;
        }

        public override void Write(object value, ProtoWriter dest)
        {
            SubItemToken token;
            int num3;
            IList list = (IList) value;
            int count = list.Count;
            bool flag = (this.options & 1) > 0;
            if (flag)
            {
                ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
                token = ProtoWriter.StartSubItem(value, dest);
                ProtoWriter.SetPackedField(this.fieldNumber, dest);
            }
            else
            {
                token = new SubItemToken();
            }
            bool flag2 = !this.SupportNull;
            for (int i = 0; i < count; i = num3 + 1)
            {
                object obj2 = list[i];
                if (flag2 && (obj2 == null))
                {
                    throw new NullReferenceException();
                }
                base.Tail.Write(obj2, dest);
                num3 = i;
            }
            if (flag)
            {
                ProtoWriter.EndSubItem(token, dest);
            }
        }

        private bool AppendToCollection
        {
            get
            {
                return ((this.options & 2) == 0);
            }
        }

        public override Type ExpectedType
        {
            get
            {
                return this.arrayType;
            }
        }

        public override bool RequiresOldValue
        {
            get
            {
                return this.AppendToCollection;
            }
        }

        public override bool ReturnsValue
        {
            get
            {
                return true;
            }
        }

        private bool SupportNull
        {
            get
            {
                return ((this.options & 4) > 0);
            }
        }
    }
}

