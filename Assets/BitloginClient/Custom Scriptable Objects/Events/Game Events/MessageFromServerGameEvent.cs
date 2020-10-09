using JSCSharpConnection;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "MessageFromServerGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "MessageFromServer",
	    order = 120)]
	public sealed class MessageFromServerGameEvent : GameEventBase<MessageFromServer>
	{
	}
}