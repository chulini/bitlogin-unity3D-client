using NBitcoin;

namespace Bitlogin
{
    /// <summary>
    /// A user account
    /// </summary>
    public class Account
    {
        public enum VerificationState
        {
            unknown = 0,
            unverified = 1,
            verified = 2
        }

        public VerificationState currentVerificationState;

        public string publicId;
        public BitcoinAddress LegacyAddress { get; private set; }

        public string messageToBeSigned { get; private set; }

        public Account(string inLegacyAddress, string inMessageToBeSigned)
        {
            LegacyAddress = BitcoinAddress.Create(inLegacyAddress, Network.Main);
            messageToBeSigned = inMessageToBeSigned;
            currentVerificationState = VerificationState.unverified;
        }


        
    }
}