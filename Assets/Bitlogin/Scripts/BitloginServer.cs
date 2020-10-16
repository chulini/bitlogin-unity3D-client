using System.Collections.Generic;
using UnityEngine;

namespace Bitlogin
{
    public class BitloginServer
    {
        float _randomSeed;
        Dictionary<string,Account> _accountsByLegacyAddress;
        
        public BitloginServer(float seed)
        {
            _randomSeed = seed;
            _accountsByLegacyAddress = new Dictionary<string, Account>();
        }

        public BitloginProtocolMessage GetOkSignThisMessage(HiMessage hiMessage)
        {
            if (_accountsByLegacyAddress.ContainsKey(hiMessage.legacyAddress))
            {
                return new ErrorMessage(100);
            }

            string messageToBeSigned = GetRandomStringToBeSignedByClient();
            OkSignThisMessage okSignThisMessage = new OkSignThisMessage(messageToBeSigned);
            Account account = new Account(hiMessage.legacyAddress, messageToBeSigned);
            _accountsByLegacyAddress.Add(hiMessage.legacyAddress, account);
            return okSignThisMessage;
        }


        string GetRandomStringToBeSignedByClient()
        {
            return (Random.Range(0,int.MaxValue)*_randomSeed).ToString(); // TODO implement this with a real source of randomness
        }


        public BitloginProtocolMessage GetAuthenticationResult(VerifyMeMessage verifyMeMessage)
        {
            if (_accountsByLegacyAddress.TryGetValue(verifyMeMessage.legacyAddress, out Account accountToBeVerified))
            {
                if (accountToBeVerified.currentVerificationState == Account.VerificationState.verified)
                {
                    return new ErrorMessage(300);
                }

                SignatureVerifier.SignatureVerificationResult signatureVerificationResult = SignatureVerifier.Verify(
                    accountToBeVerified.LegacyAddress.ToString(),
                    accountToBeVerified.messageToBeSigned,
                    verifyMeMessage.signature);
                
                if(signatureVerificationResult == SignatureVerifier.SignatureVerificationResult.SignatureIsValid)
                {
                    accountToBeVerified.currentVerificationState = Account.VerificationState.verified;
                    accountToBeVerified.publicId = verifyMeMessage.publicIdIWantToHave;
                    return new AuthenticationSuccess();
                }
                
                if (signatureVerificationResult == SignatureVerifier.SignatureVerificationResult.SignatureDoesntMatchMessage)
                {
                    return new ErrorMessage(400);
                }
                return new ErrorMessage(500);
            }
            return new ErrorMessage(200);
        }
        

        public BitloginProtocolMessage LogOutRequest(LogOutRequestMessage logOutRequestMessage)
        {
            if (_accountsByLegacyAddress.TryGetValue(logOutRequestMessage.legacyAddress, out Account account))
            {
                SignatureVerifier.SignatureVerificationResult signatureVerificationResult = SignatureVerifier.Verify(
                    logOutRequestMessage.legacyAddress,
                    $"LogOut{logOutRequestMessage.legacyAddress}",
                    logOutRequestMessage.signature);
                if(signatureVerificationResult == SignatureVerifier.SignatureVerificationResult.SignatureIsValid)
                {
                    _accountsByLegacyAddress.Remove(logOutRequestMessage.legacyAddress);
                    return new LogOutSuccessMessage();
                }
                if (signatureVerificationResult == SignatureVerifier.SignatureVerificationResult.SignatureDoesntMatchMessage)
                {
                    return new ErrorMessage(400);
                }
                if (signatureVerificationResult == SignatureVerifier.SignatureVerificationResult.StringSentIsNotASignature)
                {
                    return new ErrorMessage(500);
                }
                
            }
            
            return new ErrorMessage(600);
            
        }
            
        public Account.VerificationState GetVerificationStateOfAccount(string legacyAddress)
        {
            if (_accountsByLegacyAddress.TryGetValue(legacyAddress, out Account account))
            {
                return account.currentVerificationState;
            }

            return Account.VerificationState.unknown;
        }
        
        // TODO make tests
        public bool VerifyReceivedMessage(MessageToReceive messageToReceive)
        {
            Account.VerificationState verificationState = GetVerificationStateOfAccount(messageToReceive.legacyAddress);
            if (verificationState != Account.VerificationState.verified)
            {
                // Bitlogin assumes an unverified account send unverified messages just for security
                return false;
            }
            return SignatureVerifier.VerifyReceivedMessage(messageToReceive);
        }
    }
}