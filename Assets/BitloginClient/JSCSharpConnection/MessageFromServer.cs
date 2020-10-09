namespace JSCSharpConnection
{
    public struct MessageFromServer
    {
        public string message { get; private set; }

        public MessageFromServer(string inMessage)
        {
            message = inMessage;
        }
    }
}