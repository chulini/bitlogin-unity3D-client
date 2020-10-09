using BitloginClient.Character;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "CharacterCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Character",
	    order = 120)]
	public class CharacterCollection : Collection<Character>
	{
	}
}