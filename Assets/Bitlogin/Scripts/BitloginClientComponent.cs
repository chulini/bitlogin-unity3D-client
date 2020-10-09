using UnityEngine;

namespace Bitlogin
{
    /// <summary>
    /// Bitlogin class that can be attached to a gameObject instance 
    /// </summary>
    public class BitloginClientComponent : MonoBehaviour
    {
        public static BitloginClient bitloginClient { get; private set; }

        void Awake()
        {
            bitloginClient = new BitloginClient();
        }
        
    }
}

