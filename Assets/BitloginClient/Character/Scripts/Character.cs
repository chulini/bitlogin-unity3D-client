using ScriptableObjectArchitecture;
using UnityEngine;

namespace BitloginClient.Character
{
    /// <summary>
    /// Updates a character instantiated in the scene
    /// </summary>
    public class Character : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] CharacterCollection _allCharactersInScene;
#pragma warning restore 0649

        void OnEnable(){
            _allCharactersInScene.Add(this);
        }

        void OnDisable()
        {
            _allCharactersInScene.Remove(this);
        }
    }
}