using ScriptableObjectArchitecture;
using UnityEngine;

namespace JSCSharpConnection
{
    /// <summary>
    /// Receives messages from the server and broadcast that through an event
    /// </summary>
    public class ServerMessageReceiver : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] MessageFromServerGameEvent _messageReceivedFromServer;
#pragma warning restore 0649
        
        /// <summary>
        /// This is called from JavaScript when a server message has been received
        /// </summary>
        void MessageReceivedFromServer(string message)
        {
            Debug.Log($"C# [Received] {message}");
            _messageReceivedFromServer.Raise(new MessageFromServer(message));
        }
    }
}