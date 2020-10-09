using JSCSharpConnection;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "MessageToServerVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "MessageToServer",
	    order = 120)]
	public class MessageToServerVariable : BaseVariable<MessageToServer>
	{
	}
}