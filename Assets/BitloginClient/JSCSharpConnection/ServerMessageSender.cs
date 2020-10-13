using System.Runtime.InteropServices;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace JSCSharpConnection
{
    /// <summary>
    /// Listen to a message event and send that message to the server
    /// </summary>
    public class ServerMessageSender : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] MessageToServerGameEvent _sendMessageToServer;
#pragma warning restore 0649
        
        /// <summary>
        /// This function is executed in JavaScript to send a message to the server 
        /// </summary>
        [DllImport("__Internal")]
        private static extern void SendMessageToServer(string message);

        void OnEnable()
        {
            _sendMessageToServer.AddListener(OnSendMessageToServer);
        }
        void OnDisable()
        {
            _sendMessageToServer.RemoveListener(OnSendMessageToServer);
        }

        void OnSendMessageToServer(MessageToServer messageToSend)
        {
            string log = $"Out of C# message -> {messageToSend.message}";
            try
            {
                SendMessageToServer(messageToSend.message);    
            }
            catch 
            {
                log += " (No Receiver)";
            }
            Debug.Log(log);
        }
    }
}