using NBitcoin;

namespace Bitlogin
{
    public static class SignatureVerifier
    {
        public enum SignatureVerificationResult
        {
            SignatureDoesntMatchMessage,
            StringSentIsNotASignature,
            SignatureIsValid
        }

        public static SignatureVerificationResult Verify(string legacyAddreess, string messageToSign, string signature)
        {
            BitcoinPubKeyAddress bitcoinPubKeyAddress = new BitcoinPubKeyAddress(legacyAddreess, Network.Main);
            try
            {
                bool verified = bitcoinPubKeyAddress.VerifyMessage(messageToSign, signature);
                if (verified)
                {
                    return SignatureVerificationResult.SignatureIsValid;
                }
                return SignatureVerificationResult.SignatureDoesntMatchMessage;
            }
            catch
            {
                return SignatureVerificationResult.StringSentIsNotASignature;
            }
        }
        
        public static bool VerifyReceivedMessage(MessageToReceive messageToReceive)
        {
            SignatureVerificationResult signatureVerificationResult = Verify(
                messageToReceive.legacyAddress,
                messageToReceive.message,
                messageToReceive.signature
            );
            
            if (signatureVerificationResult == SignatureVerificationResult.SignatureIsValid)
            {
                return true;
            }
            return false;
        }
    }
}