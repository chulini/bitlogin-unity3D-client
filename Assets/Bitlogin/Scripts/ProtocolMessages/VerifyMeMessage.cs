using System;

namespace Bitlogin
{
    [Serializable]
    public class VerifyMeMessage : BitloginProtocolMessage
    {
        public string legacyAddress;
        public string signature;
        public string publicIdIWantToHave;

        public VerifyMeMessage(string inLegacyAddress, string inSignature, string inPublicIdIWantToHave)
        {
            messageName = "VerifyMe";
            signature = inSignature;
            legacyAddress = inLegacyAddress;
            publicIdIWantToHave = inPublicIdIWantToHave;
        }
    }
}