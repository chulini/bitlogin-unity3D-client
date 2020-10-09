using System;
using NBitcoin;
using UnityEngine;
using Network = NBitcoin.Network;

namespace Bitlogin
{
    /// <summary>
    /// Holds bitlogin client data and utility functions
    /// </summary>
    public class BitloginClient
    {
        public string PublicId { get; private set; }
        public string LegacyAddress { get; private set; }
        protected Mnemonic _mnemonic;
        bool _canModifyPublicId;

        /// <summary>
        /// Initialize client from a new randomly generated mnemonic
        /// </summary>
        public BitloginClient()
        {
            _canModifyPublicId = true;
            SetMnemonic(new Mnemonic(Wordlist.English, WordCount.Twelve));
        }

        /// <summary>
        /// Initialize client from a given mnemonic
        /// </summary>
        /// <param name="mnemonic">Mnemonic to initialize the client</param>
        public BitloginClient(string mnemonic)
        {
            _canModifyPublicId = true;
            SetMnemonic(new Mnemonic(mnemonic));
        }

        void SetMnemonic(Mnemonic mnemonic)
        {
            _mnemonic = mnemonic;
            LegacyAddress = _mnemonic.DeriveExtKey().GetPublicKey().GetAddress(ScriptPubKeyType.Legacy, Network.Main)
                .ToString();
            PublicId = _mnemonic.DeriveExtKey().GetPublicKey().GetAddress(ScriptPubKeyType.SegwitP2SH, Network.Main)
                .ToString();
        }

        
        /// <returns>Success</returns>
        public bool SetCustomPublicId(string customPublicId)
        {
            if (!_canModifyPublicId)
                return false;
            
            PublicId = customPublicId;
            return true;
        }

        public HiMessage GetHiMessage()
        {
            return new HiMessage(LegacyAddress);
        }

        public VerifyMeMessage GetVerifyMeMessage(OkSignThisMessage okSignThisMessage)
        {
            string signedMessage = SignMessage(okSignThisMessage.messageToSign);
            VerifyMeMessage verifyMeMessage = new VerifyMeMessage(LegacyAddress, signedMessage, PublicId);
            return verifyMeMessage;
        }

        public string SignMessage(string messageToSign)
        {
            return _mnemonic.DeriveExtKey().PrivateKey.SignMessage(messageToSign);
        }

        public LogOutRequestMessage GetLogOutRequestMessage()
        {
            string signature = SignMessage($"LogOut{LegacyAddress}");
            return new LogOutRequestMessage(LegacyAddress,signature);
        }
    }

}