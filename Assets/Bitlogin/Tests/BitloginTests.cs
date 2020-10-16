using System.Collections;
using System.Collections.Generic;
using Bitlogin;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BitloginTests
    {
        [Test]
        public void Bitlogin_SuccessLogin_CheckClientVerifiedInServer()
        {
            BitloginClient bitloginClient = new BitloginClient();
            BitloginServer bitloginServer = new BitloginServer(Random.value);

            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);
            Assert.IsTrue(okSignThisMessage is OkSignThisMessage,
                "bitloginServer.GetOkSignThisMessage() returning not a OkSignThisMessage");
            VerifyMeMessage verifyMeMessage = bitloginClient.GetVerifyMeMessage(okSignThisMessage as OkSignThisMessage);

            BitloginProtocolMessage authenticationResult = bitloginServer.GetAuthenticationResult(verifyMeMessage);
            Assert.IsTrue(authenticationResult is AuthenticationSuccess,
                "bitloginServer.GetAuthenticationResult() returning not a AuthenticationSuccess");
            Account.VerificationState currentClientVerificationStateInServer =
                bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.verified);
        }
        
        [Test]
        public void Bitlogin_FailedLogin_ClientAlreadyInTheSystem()
        {
            BitloginClient bitloginClient = new BitloginClient();
            BitloginServer bitloginServer = new BitloginServer(Random.value);

            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);
            Assert.IsInstanceOf<OkSignThisMessage>(okSignThisMessage,
                "bitloginServer.GetOkSignThisMessage() returning not a OkSignThisMessage");
            BitloginProtocolMessage okSignThisMessage2 = bitloginServer.GetOkSignThisMessage(hiMessage);
            Assert.IsInstanceOf<ErrorMessage>(okSignThisMessage2,
                "bitloginServer.GetOkSignThisMessage() returning not a ErrorMessage when GettingOkSignThisMessage twice");
            Assert.IsTrue((okSignThisMessage2 as ErrorMessage)?.errorCode == 100,
                "bitloginServer.GetOkSignThisMessage() returning not a ErrorMessage 100 when GettingOkSignThisMessage twice");

            Account.VerificationState currentClientVerificationStateInServer =
                bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.unverified);
        }

        [Test]
        public void Bitlogin_FailedLogin_ClientSendingASignatureOfAWrongMessage()
        {
            BitloginClient bitloginClient = new BitloginClient();
            BitloginServer bitloginServer = new BitloginServer(Random.value);

            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);

            VerifyMeMessage verifyMeMessage = bitloginClient.GetVerifyMeMessage(okSignThisMessage as OkSignThisMessage);

            verifyMeMessage.signature =
                bitloginClient.SignMessage("this is a wrong message to be signed" + Random.value.ToString());

            BitloginProtocolMessage authenticationResult = bitloginServer.GetAuthenticationResult(verifyMeMessage);
            Assert.IsInstanceOf<ErrorMessage>(authenticationResult,
                "Client sending fake signature is returning authentication success");
            Assert.IsTrue((authenticationResult as ErrorMessage).errorCode == 400,
                "bitloginServer.GetAuthenticationResult() returning not a error code 400");

            Account.VerificationState currentClientVerificationStateInServer =
                bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.unverified);
        }

        [Test]
        public void Bitlogin_FailedLogin_ClientSendingARandomSignature()
        {
            BitloginClient bitloginClient = new BitloginClient();
            BitloginServer bitloginServer = new BitloginServer(Random.value);

            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);

            VerifyMeMessage verifyMeMessage = bitloginClient.GetVerifyMeMessage(okSignThisMessage as OkSignThisMessage);
            verifyMeMessage.signature = "this is not even a signature " + Random.value.ToString();

            BitloginProtocolMessage authenticationResult = bitloginServer.GetAuthenticationResult(verifyMeMessage);
            Assert.IsInstanceOf<ErrorMessage>(authenticationResult,
                "Client sending fake signature is returning authentication success");
            Assert.IsTrue((authenticationResult as ErrorMessage).errorCode == 500,
                "bitloginServer.GetAuthenticationResult() returning not a error code 500");

            Account.VerificationState currentClientVerificationStateInServer =
                bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.unverified);
        }

        [Test]
        public void Bitlogin_FailedLogin_ClientTryingToVerifyBeforeHi()
        {
            BitloginClient bitloginClient = new BitloginClient();
            BitloginServer bitloginServer = new BitloginServer(Random.value);

            string messageSignature = bitloginClient.SignMessage("Message to sign");
            VerifyMeMessage verifyMeMessage = new VerifyMeMessage(bitloginClient.LegacyAddress, messageSignature, bitloginClient.PublicId);

            BitloginProtocolMessage authenticationResult = bitloginServer.GetAuthenticationResult(verifyMeMessage);
            Assert.IsInstanceOf<ErrorMessage>(authenticationResult,
                "Client sending fake signature is returning authentication success");
            Assert.IsTrue((authenticationResult as ErrorMessage).errorCode == 200,
                "bitloginServer.GetAuthenticationResult() returning not a error code 200");

            Account.VerificationState currentClientVerificationStateInServer = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.unknown);
        }
        
        [Test]
        public void Bitlogin_SuccessLoginAndLogout()
        {
            BitloginClient bitloginClient = new BitloginClient();
            BitloginServer bitloginServer = new BitloginServer(Random.value);

            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);
            VerifyMeMessage verifyMeMessage = bitloginClient.GetVerifyMeMessage(okSignThisMessage as OkSignThisMessage);
            bitloginServer.GetAuthenticationResult(verifyMeMessage);
            
            Account.VerificationState currentClientVerificationStateInServer = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.verified);

            LogOutRequestMessage logOutRequestMessage = bitloginClient.GetLogOutRequestMessage();
            BitloginProtocolMessage logoutResult = bitloginServer.LogOutRequest(logOutRequestMessage);
            
            Assert.IsInstanceOf<LogOutSuccessMessage>(logoutResult);
            Account.VerificationState currentClientVerificationStateInServerAfterLogOut = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServerAfterLogOut == Account.VerificationState.unknown);
            
        }
        
        [Test]
        public void Bitlogin_FailedLogout_ClientWasNeverLoggedIn()
        {
            BitloginServer bitloginServer = new BitloginServer(Random.value);
            BitloginClient bitloginClient = new BitloginClient();
            Account.VerificationState currentClientVerificationStateInServer = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.unknown);
            
            LogOutRequestMessage logOutRequestMessage = bitloginClient.GetLogOutRequestMessage();
            BitloginProtocolMessage logOutResponse = bitloginServer.LogOutRequest(logOutRequestMessage);
            Assert.IsInstanceOf<ErrorMessage>(logOutResponse);
            Assert.IsTrue(((ErrorMessage) logOutResponse).errorCode == 600);
            
            Account.VerificationState currentClientVerificationStateInServerAfterLogOut = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServerAfterLogOut == Account.VerificationState.unknown);
        }
        
        [Test]
        public void Bitlogin_FailedLogout_ClientSendingRandomStringAsSignature()
        {
            BitloginServer bitloginServer = new BitloginServer(Random.value);
            BitloginClient bitloginClient = new BitloginClient();
            
            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);
            VerifyMeMessage verifyMeMessage = bitloginClient.GetVerifyMeMessage(okSignThisMessage as OkSignThisMessage);
            bitloginServer.GetAuthenticationResult(verifyMeMessage);
            
            Account.VerificationState currentClientVerificationStateInServer = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.verified);
            
            LogOutRequestMessage logOutRequestMessage = bitloginClient.GetLogOutRequestMessage();
            logOutRequestMessage.signature = "this is not even a signature " + Random.value.ToString();
            
            BitloginProtocolMessage logOutResponse = bitloginServer.LogOutRequest(logOutRequestMessage);
            Assert.IsInstanceOf<ErrorMessage>(logOutResponse);
            Assert.IsTrue(((ErrorMessage) logOutResponse).errorCode == 500);
            
            Account.VerificationState currentClientVerificationStateInServerAfterLogOut = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServerAfterLogOut == Account.VerificationState.verified);
        }
        
        [Test]
        public void Bitlogin_FailedLogout_ClientSendingFakeSignature()
        {
            BitloginServer bitloginServer = new BitloginServer(Random.value);
            BitloginClient bitloginClient = new BitloginClient();
            
            HiMessage hiMessage = bitloginClient.GetHiMessage();
            BitloginProtocolMessage okSignThisMessage = bitloginServer.GetOkSignThisMessage(hiMessage);
            VerifyMeMessage verifyMeMessage = bitloginClient.GetVerifyMeMessage(okSignThisMessage as OkSignThisMessage);
            bitloginServer.GetAuthenticationResult(verifyMeMessage);
            
            Account.VerificationState currentClientVerificationStateInServer = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServer == Account.VerificationState.verified);
            
            LogOutRequestMessage logOutRequestMessage = bitloginClient.GetLogOutRequestMessage();
            logOutRequestMessage.signature = bitloginClient.SignMessage("Wrong message to sign");
            
            BitloginProtocolMessage logOutResponse = bitloginServer.LogOutRequest(logOutRequestMessage);
            Assert.IsInstanceOf<ErrorMessage>(logOutResponse);
            Assert.IsTrue(((ErrorMessage) logOutResponse).errorCode == 400);
            
            Account.VerificationState currentClientVerificationStateInServerAfterLogOut = bitloginServer.GetVerificationStateOfAccount(bitloginClient.LegacyAddress);
            Assert.IsTrue(currentClientVerificationStateInServerAfterLogOut == Account.VerificationState.verified);
        }
     
    }
}
