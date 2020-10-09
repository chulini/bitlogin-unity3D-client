using System;

namespace Bitlogin
{
    [Serializable]
    public class ErrorMessage : BitloginProtocolMessage
    {
        public int errorCode;
        public string errorMessage;


        public ErrorMessage(int inErrorCode)
        {
            messageName = "Error";
            errorCode = inErrorCode;
            errorMessage = ErrorCodes.GetErrorMessage(errorCode);
        }
    }
    
}