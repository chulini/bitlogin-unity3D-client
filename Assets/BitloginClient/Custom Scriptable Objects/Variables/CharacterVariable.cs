using BitloginClient.Character;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "CharacterVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Character",
	    order = 120)]
	public class CharacterVariable : BaseVariable<Character>
	{
	}
}