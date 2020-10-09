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
    }
}