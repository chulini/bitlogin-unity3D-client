using System;
using System.Collections.Generic;

namespace Bitlogin
{
    public static class ErrorCodes
    {
        public static Dictionary<int, string> allErrorCodes;

        public static string GetErrorMessage(int errorCode)
        {
            if (allErrorCodes == null)
            {
                allErrorCodes = new Dictionary<int, string>();
                allErrorCodes.Add(100, "This legacy address is already in the system");
                allErrorCodes.Add(200, "Server doesn't know anything about you. Please send a hi message first.");
                allErrorCodes.Add(300, "Can't verify an already verified account");
                allErrorCodes.Add(400, "Message signature doesn't match the sent message");
                allErrorCodes.Add(500, "The string you are sending as signature is not even a signature");
                allErrorCodes.Add(600, "Can't LogOut an unknown account");
            }

            if (allErrorCodes.TryGetValue(errorCode, out string errorMessage))
            {
                return errorMessage;
            }
            else
            {
                throw new NullReferenceException($"Errorcode number {errorCode} doesn't exist");
            }
        }

    }
}