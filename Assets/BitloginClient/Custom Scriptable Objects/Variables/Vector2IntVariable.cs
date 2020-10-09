using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "Vector2IntVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Vector2Int",
	    order = 120)]
	public class Vector2IntVariable : BaseVariable<Vector2Int>
	{
	}
}