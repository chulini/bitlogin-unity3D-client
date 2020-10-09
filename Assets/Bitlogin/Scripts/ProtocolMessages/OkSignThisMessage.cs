using System;

namespace Bitlogin
{
    [Serializable]
    public class OkSignThisMessage : BitloginProtocolMessage
    {
        public string messageToSign;

        public OkSignThisMessage(string inMessageToSign)
        {
            messageName = "OkSignThis";
            messageToSign = inMessageToSign;
        }
    }
}