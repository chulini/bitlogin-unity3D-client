namespace Bitlogin
{
    public class MessageToReceive : BitloginProtocolMessage
    {
        public string legacyAddress;
        public string signature;
        public string message;
        public MessageToReceive(string inLegacyAddress, string inMessage,  string inSignature)
        {
            messageName = "MessageToReceive";
            legacyAddress = inLegacyAddress;
            message = inMessage; 
            signature = inSignature;
        }
    }
}