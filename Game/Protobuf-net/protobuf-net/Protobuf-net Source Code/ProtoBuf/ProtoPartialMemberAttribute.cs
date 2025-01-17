﻿namespace ProtoBuf
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=false)]
    public sealed class ProtoPartialMemberAttribute : ProtoMemberAttribute
    {
        private readonly string memberName;

        public ProtoPartialMemberAttribute(int tag, string memberName) : base(tag)
        {
            if (Helpers.IsNullOrEmpty(memberName))
            {
                throw new ArgumentNullException("memberName");
            }
            this.memberName = memberName;
        }

        public string MemberName
        {
            get
            {
                return this.memberName;
            }
        }
    }
}

