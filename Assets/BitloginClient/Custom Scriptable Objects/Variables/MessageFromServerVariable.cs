using JSCSharpConnection;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "MessageFromServerVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "MessageFromServer",
	    order = 120)]
	public class MessageFromServerVariable : BaseVariable<MessageFromServer>
	{
	}
}