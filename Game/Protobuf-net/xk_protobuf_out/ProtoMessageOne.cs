// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: proto_message_one.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace XkProtobufData {

  /// <summary>Holder for reflection information generated from proto_message_one.proto</summary>
  public static partial class ProtoMessageOneReflection {

    #region Descriptor
    /// <summary>File descriptor for proto_message_one.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProtoMessageOneReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chdwcm90b19tZXNzYWdlX29uZS5wcm90bxIQeGtfcHJvdG9idWZfZGF0YRoS",
            "cHJvdG9fc3RydWN0LnByb3RvIlMKDHB1c2hDaGF0SW5mbxIOCgZyZXN1bHQY",
            "ASABKA0SMwoIY2hhdEluZm8YAiABKAsyIS54a19wcm90b2J1Zl9kYXRhLnN0",
            "cnVjdF9DaGF0SW5mbyJfCg5wdXNoUGxheWVySW5mbxIOCgZyZXN1bHQYASAB",
            "KA0SPQoKcGxheWVySW5mbxgCIAEoCzIpLnhrX3Byb3RvYnVmX2RhdGEuc3Ry",
            "dWN0X1BsYXllckRldGFpbEluZm8iZAoTcHVzaE9odGVyUGxheWVySW5mbxIO",
            "CgZyZXN1bHQYASABKA0SPQoKcGxheWVySW5mbxgCIAEoCzIpLnhrX3Byb3Rv",
            "YnVmX2RhdGEuc3RydWN0X1BsYXllckRldGFpbEluZm9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::XkProtobufData.ProtoStructReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::XkProtobufData.pushChatInfo), global::XkProtobufData.pushChatInfo.Parser, new[]{ "Result", "ChatInfo" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::XkProtobufData.pushPlayerInfo), global::XkProtobufData.pushPlayerInfo.Parser, new[]{ "Result", "PlayerInfo" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::XkProtobufData.pushOhterPlayerInfo), global::XkProtobufData.pushOhterPlayerInfo.Parser, new[]{ "Result", "PlayerInfo" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// 5001,Push Chat Info
  /// </summary>
  public sealed partial class pushChatInfo : pb::IMessage<pushChatInfo> {
    private static readonly pb::MessageParser<pushChatInfo> _parser = new pb::MessageParser<pushChatInfo>(() => new pushChatInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pushChatInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::XkProtobufData.ProtoMessageOneReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushChatInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushChatInfo(pushChatInfo other) : this() {
      result_ = other.result_;
      ChatInfo = other.chatInfo_ != null ? other.ChatInfo.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushChatInfo Clone() {
      return new pushChatInfo(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private uint result_;
    /// <summary>
    /// 1:ok 2:error
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "chatInfo" field.</summary>
    public const int ChatInfoFieldNumber = 2;
    private global::XkProtobufData.struct_ChatInfo chatInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::XkProtobufData.struct_ChatInfo ChatInfo {
      get { return chatInfo_; }
      set {
        chatInfo_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pushChatInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pushChatInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (!object.Equals(ChatInfo, other.ChatInfo)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      if (chatInfo_ != null) hash ^= ChatInfo.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Result);
      }
      if (chatInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(ChatInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Result);
      }
      if (chatInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ChatInfo);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pushChatInfo other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      if (other.chatInfo_ != null) {
        if (chatInfo_ == null) {
          chatInfo_ = new global::XkProtobufData.struct_ChatInfo();
        }
        ChatInfo.MergeFrom(other.ChatInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Result = input.ReadUInt32();
            break;
          }
          case 18: {
            if (chatInfo_ == null) {
              chatInfo_ = new global::XkProtobufData.struct_ChatInfo();
            }
            input.ReadMessage(chatInfo_);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /// 5002
  /// </summary>
  public sealed partial class pushPlayerInfo : pb::IMessage<pushPlayerInfo> {
    private static readonly pb::MessageParser<pushPlayerInfo> _parser = new pb::MessageParser<pushPlayerInfo>(() => new pushPlayerInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pushPlayerInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::XkProtobufData.ProtoMessageOneReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushPlayerInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushPlayerInfo(pushPlayerInfo other) : this() {
      result_ = other.result_;
      PlayerInfo = other.playerInfo_ != null ? other.PlayerInfo.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushPlayerInfo Clone() {
      return new pushPlayerInfo(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private uint result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "playerInfo" field.</summary>
    public const int PlayerInfoFieldNumber = 2;
    private global::XkProtobufData.struct_PlayerDetailInfo playerInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::XkProtobufData.struct_PlayerDetailInfo PlayerInfo {
      get { return playerInfo_; }
      set {
        playerInfo_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pushPlayerInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pushPlayerInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (!object.Equals(PlayerInfo, other.PlayerInfo)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      if (playerInfo_ != null) hash ^= PlayerInfo.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Result);
      }
      if (playerInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(PlayerInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Result);
      }
      if (playerInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PlayerInfo);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pushPlayerInfo other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      if (other.playerInfo_ != null) {
        if (playerInfo_ == null) {
          playerInfo_ = new global::XkProtobufData.struct_PlayerDetailInfo();
        }
        PlayerInfo.MergeFrom(other.PlayerInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Result = input.ReadUInt32();
            break;
          }
          case 18: {
            if (playerInfo_ == null) {
              playerInfo_ = new global::XkProtobufData.struct_PlayerDetailInfo();
            }
            input.ReadMessage(playerInfo_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class pushOhterPlayerInfo : pb::IMessage<pushOhterPlayerInfo> {
    private static readonly pb::MessageParser<pushOhterPlayerInfo> _parser = new pb::MessageParser<pushOhterPlayerInfo>(() => new pushOhterPlayerInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pushOhterPlayerInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::XkProtobufData.ProtoMessageOneReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushOhterPlayerInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushOhterPlayerInfo(pushOhterPlayerInfo other) : this() {
      result_ = other.result_;
      PlayerInfo = other.playerInfo_ != null ? other.PlayerInfo.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pushOhterPlayerInfo Clone() {
      return new pushOhterPlayerInfo(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private uint result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "playerInfo" field.</summary>
    public const int PlayerInfoFieldNumber = 2;
    private global::XkProtobufData.struct_PlayerDetailInfo playerInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::XkProtobufData.struct_PlayerDetailInfo PlayerInfo {
      get { return playerInfo_; }
      set {
        playerInfo_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pushOhterPlayerInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pushOhterPlayerInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (!object.Equals(PlayerInfo, other.PlayerInfo)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      if (playerInfo_ != null) hash ^= PlayerInfo.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Result);
      }
      if (playerInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(PlayerInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Result);
      }
      if (playerInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PlayerInfo);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pushOhterPlayerInfo other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      if (other.playerInfo_ != null) {
        if (playerInfo_ == null) {
          playerInfo_ = new global::XkProtobufData.struct_PlayerDetailInfo();
        }
        PlayerInfo.MergeFrom(other.PlayerInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Result = input.ReadUInt32();
            break;
          }
          case 18: {
            if (playerInfo_ == null) {
              playerInfo_ = new global::XkProtobufData.struct_PlayerDetailInfo();
            }
            input.ReadMessage(playerInfo_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
