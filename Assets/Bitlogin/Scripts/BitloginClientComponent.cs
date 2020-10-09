using UnityEngine;

namespace Bitlogin
{
    /// <summary>
    /// Bitlogin class that can be attached to a gameObject instance 
    /// </summary>
    public class BitloginClientComponent : MonoBehaviour
    {
        static BitloginClientComponent instance;
        public static BitloginClient bitloginClient { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            bitloginClient = new BitloginClient();
        }

        public static void Login()
        {
            
        } 
        
    }
}

