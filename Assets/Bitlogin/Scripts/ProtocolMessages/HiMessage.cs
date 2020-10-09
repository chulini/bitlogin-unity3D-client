using System;

namespace Bitlogin
{
    [Serializable]
    public class HiMessage : BitloginProtocolMessage
    {
        public string legacyAddress;

        public HiMessage(string inLegacyAddress)
        {
            messageName = "Hi";
            legacyAddress = inLegacyAddress;
        }
    }
}