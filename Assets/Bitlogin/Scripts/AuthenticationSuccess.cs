using System;

namespace Bitlogin
{
    [Serializable]
    public class AuthenticationSuccess : BitloginProtocolMessage
    {
        public AuthenticationSuccess()
        {
            messageName = "AuthenticationSuccess";
        }

    }
}