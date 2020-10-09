using JSCSharpConnection;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class MessageToServerReference : BaseReference<MessageToServer, MessageToServerVariable>
	{
	    public MessageToServerReference() : base() { }
	    public MessageToServerReference(MessageToServer value) : base(value) { }
	}
}