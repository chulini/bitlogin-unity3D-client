using JSCSharpConnection;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "MessageToServerGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "MessageToServer",
	    order = 120)]
	public sealed class MessageToServerGameEvent : GameEventBase<MessageToServer>
	{
	}
}