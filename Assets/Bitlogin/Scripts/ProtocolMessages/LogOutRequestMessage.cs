using System;

namespace Bitlogin
{
    [Serializable]
    public class LogOutRequestMessage : BitloginProtocolMessage
    {
        public string legacyAddress;
        public string signature;
        public LogOutRequestMessage(string inLegacyAddress, string inSignature)
        {
            messageName = "LogOut";
            legacyAddress = inLegacyAddress;
            signature = inSignature;
        }
    }
}