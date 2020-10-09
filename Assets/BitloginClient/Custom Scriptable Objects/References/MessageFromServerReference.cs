using JSCSharpConnection;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class MessageFromServerReference : BaseReference<MessageFromServer, MessageFromServerVariable>
	{
	    public MessageFromServerReference() : base() { }
	    public MessageFromServerReference(MessageFromServer value) : base(value) { }
	}
}