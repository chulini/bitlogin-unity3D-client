using System;
using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace JSCSharpConnection
{
    public class DummMessage : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] MessageFromServerGameEvent _messageReceivedFromServer;
        [SerializeField] MessageToServerGameEvent _sendMessageToServer;
#pragma warning restore 0649

        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(5f);
                _sendMessageToServer.Raise(new MessageToServer(Time.time.ToString()));
            }
            
        }
    }
}